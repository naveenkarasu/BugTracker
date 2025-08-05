const express = require('express');
const { createServer } = require('http');
const { Server } = require('socket.io');
const cors = require('cors');
const helmet = require('helmet');
const rateLimit = require('express-rate-limit');
const jwt = require('jsonwebtoken');
const winston = require('winston');
const { register, collectDefaultMetrics, Counter, Gauge, Histogram } = require('prom-client');
const { Pool } = require('pg');
require('dotenv').config();

// Initialize Express app
const app = express();
const server = createServer(app);
const io = new Server(server, {
  cors: {
    origin: process.env.CLIENT_URL || "http://localhost:3000",
    methods: ["GET", "POST"],
    credentials: true
  }
});

// Configure Winston logging
const logger = winston.createLogger({
  level: 'info',
  format: winston.format.combine(
    winston.format.timestamp(),
    winston.format.errors({ stack: true }),
    winston.format.json()
  ),
  defaultMeta: { service: 'socket-service' },
  transports: [
    new winston.transports.File({ filename: 'logs/error.log', level: 'error' }),
    new winston.transports.File({ filename: 'logs/combined.log' }),
    new winston.transports.Console({
      format: winston.format.combine(
        winston.format.colorize(),
        winston.format.simple()
      )
    })
  ]
});

// Configure Prometheus metrics
collectDefaultMetrics();
const socketConnections = new Gauge({
  name: 'socket_connections_total',
  help: 'Total number of socket connections'
});
const messagesReceived = new Counter({
  name: 'socket_messages_received_total',
  help: 'Total number of messages received'
});
const messageLatency = new Histogram({
  name: 'socket_message_latency_seconds',
  help: 'Message processing latency in seconds'
});

// Database connection
const pool = new Pool({
  connectionString: process.env.DATABASE_URL,
  ssl: process.env.NODE_ENV === 'production' ? { rejectUnauthorized: false } : false
});

// Middleware
app.use(helmet());
app.use(cors({
  origin: process.env.CLIENT_URL || "http://localhost:3000",
  credentials: true
}));

// Rate limiting
const limiter = rateLimit({
  windowMs: 15 * 60 * 1000, // 15 minutes
  max: 100 // limit each IP to 100 requests per windowMs
});
app.use(limiter);

app.use(express.json());

// Health check endpoint
app.get('/health', (req, res) => {
  res.json({ status: 'healthy', timestamp: new Date().toISOString() });
});

// Prometheus metrics endpoint
app.get('/metrics', async (req, res) => {
  try {
    res.set('Content-Type', register.contentType);
    res.end(await register.metrics());
  } catch (err) {
    res.status(500).end(err);
  }
});

// JWT verification middleware
const verifyToken = (token) => {
  try {
    return jwt.verify(token, process.env.JWT_SECRET);
  } catch (error) {
    logger.error('JWT verification failed:', error.message);
    return null;
  }
};

// Socket.IO authentication middleware
io.use((socket, next) => {
  const token = socket.handshake.auth.token;
  if (!token) {
    return next(new Error('Authentication error: No token provided'));
  }

  const decoded = verifyToken(token);
  if (!decoded) {
    return next(new Error('Authentication error: Invalid token'));
  }

  socket.userId = decoded.sub;
  socket.userEmail = decoded.email;
  socket.userName = decoded.name;
  socket.userRoles = decoded.roles || [];
  
  logger.info(`User authenticated: ${socket.userEmail} (${socket.userId})`);
  next();
});

// Socket.IO connection handling
io.on('connection', (socket) => {
  socketConnections.inc();
  logger.info(`User connected: ${socket.userEmail} (${socket.userId})`);

  // Join user to their project rooms
  joinUserToProjects(socket);

  // Handle bug updates
  socket.on('bug:update', async (data) => {
    const startTime = Date.now();
    messagesReceived.inc();
    
    try {
      logger.info(`Bug update received from ${socket.userEmail}:`, data);
      
      // Broadcast to project room
      socket.to(`project:${data.projectId}`).emit('bug:updated', {
        ...data,
        updatedBy: {
          id: socket.userId,
          email: socket.userEmail,
          name: socket.userName
        },
        timestamp: new Date().toISOString()
      });

      // Record metrics
      const latency = (Date.now() - startTime) / 1000;
      messageLatency.observe(latency);
      
    } catch (error) {
      logger.error('Error handling bug update:', error);
      socket.emit('error', { message: 'Failed to process bug update' });
    }
  });

  // Handle comment updates
  socket.on('comment:add', async (data) => {
    const startTime = Date.now();
    messagesReceived.inc();
    
    try {
      logger.info(`Comment added by ${socket.userEmail}:`, data);
      
      // Broadcast to project room
      socket.to(`project:${data.projectId}`).emit('comment:added', {
        ...data,
        addedBy: {
          id: socket.userId,
          email: socket.userEmail,
          name: socket.userName
        },
        timestamp: new Date().toISOString()
      });

      // Record metrics
      const latency = (Date.now() - startTime) / 1000;
      messageLatency.observe(latency);
      
    } catch (error) {
      logger.error('Error handling comment add:', error);
      socket.emit('error', { message: 'Failed to process comment' });
    }
  });

  // Handle project updates
  socket.on('project:update', async (data) => {
    const startTime = Date.now();
    messagesReceived.inc();
    
    try {
      logger.info(`Project update from ${socket.userEmail}:`, data);
      
      // Broadcast to project room
      socket.to(`project:${data.projectId}`).emit('project:updated', {
        ...data,
        updatedBy: {
          id: socket.userId,
          email: socket.userEmail,
          name: socket.userName
        },
        timestamp: new Date().toISOString()
      });

      // Record metrics
      const latency = (Date.now() - startTime) / 1000;
      messageLatency.observe(latency);
      
    } catch (error) {
      logger.error('Error handling project update:', error);
      socket.emit('error', { message: 'Failed to process project update' });
    }
  });

  // Handle user typing
  socket.on('typing:start', (data) => {
    socket.to(`project:${data.projectId}`).emit('typing:started', {
      userId: socket.userId,
      userName: socket.userName,
      projectId: data.projectId
    });
  });

  socket.on('typing:stop', (data) => {
    socket.to(`project:${data.projectId}`).emit('typing:stopped', {
      userId: socket.userId,
      userName: socket.userName,
      projectId: data.projectId
    });
  });

  // Handle disconnection
  socket.on('disconnect', () => {
    socketConnections.dec();
    logger.info(`User disconnected: ${socket.userEmail} (${socket.userId})`);
  });
});

// Function to join user to their project rooms
async function joinUserToProjects(socket) {
  try {
    const query = `
      SELECT DISTINCT pm.project_id 
      FROM project_members pm 
      WHERE pm.user_id = $1
    `;
    
    const result = await pool.query(query, [socket.userId]);
    
    result.rows.forEach(row => {
      socket.join(`project:${row.project_id}`);
      logger.info(`User ${socket.userEmail} joined project room: ${row.project_id}`);
    });
    
  } catch (error) {
    logger.error('Error joining user to projects:', error);
  }
}

// Error handling
process.on('uncaughtException', (error) => {
  logger.error('Uncaught Exception:', error);
  process.exit(1);
});

process.on('unhandledRejection', (reason, promise) => {
  logger.error('Unhandled Rejection at:', promise, 'reason:', reason);
  process.exit(1);
});

// Graceful shutdown
process.on('SIGTERM', () => {
  logger.info('SIGTERM received, shutting down gracefully');
  server.close(() => {
    logger.info('Process terminated');
    process.exit(0);
  });
});

// Start server
const PORT = process.env.PORT || 4000;
server.listen(PORT, () => {
  logger.info(`Socket.IO server running on port ${PORT}`);
  logger.info(`Environment: ${process.env.NODE_ENV || 'development'}`);
}); 