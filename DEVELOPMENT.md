# Bug Tracker Lite - Development Guide

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- **Docker & Docker Compose** - For containerized development
- **.NET 8 SDK** - For .NET API development
- **Node.js 18+** - For Node.js service and React development
- **Git** - For version control
- **VS Code** (recommended) - With extensions for C#, TypeScript, and Docker

### Quick Start

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd BugTracker
   ```

2. **Start all services with Docker**
   ```bash
   docker-compose up -d
   ```

3. **Access the application**
   - Frontend: http://localhost:3000
   - .NET API: http://localhost:5000
   - Socket.IO Service: http://localhost:4000
   - Prometheus: http://localhost:9090
   - Grafana: http://localhost:3001 (admin/admin)

## 🏗️ Architecture Overview

### Services

1. **Frontend (React + TypeScript)**
   - Port: 3000
   - Modern React with hooks, TypeScript, Material-UI
   - Real-time updates via Socket.IO
   - State management with Zustand

2. **Backend API (.NET Core)**
   - Port: 5000
   - ASP.NET Core 8 with Identity
   - JWT authentication
   - Entity Framework Core with PostgreSQL
   - Serilog for structured logging
   - Prometheus metrics

3. **Real-time Service (Node.js + Socket.IO)**
   - Port: 4000
   - Express.js with Socket.IO
   - JWT authentication
   - Winston logging
   - Prometheus metrics

4. **Database (PostgreSQL)**
   - Port: 5432
   - Persistent data storage
   - User management and bug tracking data

5. **Monitoring Stack**
   - **Prometheus**: Metrics collection (Port: 9090)
   - **Grafana**: Metrics visualization (Port: 3001)

## 🔧 Development Workflow

### Branch Strategy

We use a feature branch workflow:

```bash
main (stable)
├── develop (integration)
├── feature/user-authentication
├── feature/bug-tracking
└── hotfix/critical-fix
```

### Development Process

1. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes**
   - Follow the coding standards below
   - Write tests for new functionality
   - Update documentation

3. **Test your changes**
   ```bash
   # Test .NET API
   cd server-dotnet
   dotnet test

   # Test Node.js service
   cd server-node
   npm test

   # Test React app
   cd client
   npm test
   ```

4. **Create a Pull Request**
   - Target the `develop` branch
   - Include a clear description
   - Reference any related issues

### Coding Standards

#### .NET (C#)
- Use C# 12 features where appropriate
- Follow Microsoft C# coding conventions
- Use async/await for all I/O operations
- Implement proper error handling
- Add XML documentation for public APIs

#### Node.js (TypeScript)
- Use TypeScript for type safety
- Follow ESLint configuration
- Use async/await instead of callbacks
- Implement proper error handling
- Add JSDoc comments for functions

#### React (TypeScript)
- Use functional components with hooks
- Follow React best practices
- Use TypeScript for all components
- Implement proper error boundaries
- Use Material-UI components consistently

## 📁 Project Structure

```
BugTracker/
├── client/                 # React frontend
│   ├── src/
│   │   ├── components/     # Reusable components
│   │   ├── pages/         # Page components
│   │   ├── services/      # API services
│   │   ├── hooks/         # Custom hooks
│   │   ├── store/         # State management
│   │   └── types/         # TypeScript types
│   ├── public/            # Static assets
│   └── package.json
├── server-dotnet/          # .NET API
│   ├── Controllers/       # API controllers
│   ├── Models/           # Data models
│   ├── Services/         # Business logic
│   ├── Data/             # Database context
│   ├── DTOs/             # Data transfer objects
│   └── BugTracker.API.csproj
├── server-node/           # Socket.IO service
│   ├── server.js         # Main server file
│   ├── package.json
│   └── Dockerfile
├── prometheus/            # Prometheus config
│   └── prometheus.yml
├── grafana/              # Grafana config
│   ├── provisioning/
│   └── dashboards/
├── docker-compose.yml    # Service orchestration
└── README.md
```

## 🧪 Testing

### Running Tests

```bash
# .NET Tests
cd server-dotnet
dotnet test

# Node.js Tests
cd server-node
npm test

# React Tests
cd client
npm test
```

### Test Coverage

- Aim for >80% code coverage
- Write unit tests for all business logic
- Write integration tests for API endpoints
- Write E2E tests for critical user flows

## 📊 Monitoring & Observability

### Metrics

The application exposes metrics on `/metrics` endpoints:

- **.NET API**: http://localhost:5000/metrics
- **Node.js Service**: http://localhost:4000/metrics

### Logging

- **.NET**: Serilog with structured logging
- **Node.js**: Winston with JSON formatting
- Logs are written to files and console

### Health Checks

- **.NET API**: http://localhost:5000/health
- **Node.js Service**: http://localhost:4000/health

## 🔐 Authentication & Authorization

### JWT Configuration

The application uses JWT tokens for authentication:

- **Secret Key**: Configured in environment variables
- **Expiration**: 60 minutes (configurable)
- **Issuer**: bugtracker-api
- **Audience**: bugtracker-client

### Roles

- **Admin**: Full system access
- **Developer**: Can create/edit bugs, manage projects
- **Tester**: Can create bugs, add comments
- **Viewer**: Read-only access

## 🐳 Docker Development

### Development Commands

```bash
# Start all services
docker-compose up -d

# View logs
docker-compose logs -f

# Stop all services
docker-compose down

# Rebuild specific service
docker-compose build api-dotnet

# Access service shell
docker-compose exec api-dotnet bash
```

### Environment Variables

Create `.env` files for local development:

```bash
# .env
NODE_ENV=development
DATABASE_URL=postgresql://bugtracker_user:bugtracker_password@localhost:5432/bugtracker
JWT_SECRET=your-super-secret-jwt-key-change-in-production
```

## 🚀 Deployment

### Production Deployment

1. **Build production images**
   ```bash
   docker-compose -f docker-compose.prod.yml build
   ```

2. **Deploy to production**
   ```bash
   docker-compose -f docker-compose.prod.yml up -d
   ```

### Environment Configuration

- Use environment variables for configuration
- Never commit secrets to version control
- Use Docker secrets for sensitive data in production

## 🤝 Collaboration Guidelines

### Code Review Process

1. **Self-review** your code before submitting
2. **Request review** from at least one team member
3. **Address feedback** promptly
4. **Merge only after approval**

### Communication

- Use GitHub Issues for bug reports and feature requests
- Use Pull Request descriptions for detailed discussions
- Keep commit messages clear and descriptive

### Documentation

- Update README.md for user-facing changes
- Update DEVELOPMENT.md for developer-facing changes
- Add inline comments for complex logic
- Document API changes in Swagger

## 🐛 Troubleshooting

### Common Issues

1. **Database connection errors**
   - Check if PostgreSQL is running
   - Verify connection string
   - Ensure database exists

2. **JWT authentication issues**
   - Verify JWT secret is consistent across services
   - Check token expiration
   - Validate token format

3. **Socket.IO connection issues**
   - Check CORS configuration
   - Verify JWT token in socket connection
   - Check network connectivity

### Debug Mode

Enable debug logging:

```bash
# .NET
export ASPNETCORE_ENVIRONMENT=Development

# Node.js
export NODE_ENV=development
export DEBUG=socket.io:*

# React
export REACT_APP_DEBUG=true
```

## 📚 Learning Resources

### .NET Core
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Identity Framework](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)

### Node.js & Socket.IO
- [Socket.IO Documentation](https://socket.io/docs/)
- [Express.js Guide](https://expressjs.com/en/guide/routing.html)
- [Winston Logging](https://github.com/winstonjs/winston)

### React & TypeScript
- [React Documentation](https://reactjs.org/docs/getting-started.html)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)
- [Material-UI](https://mui.com/getting-started/usage/)

### Monitoring
- [Prometheus Documentation](https://prometheus.io/docs/)
- [Grafana Documentation](https://grafana.com/docs/)

## 🆘 Support

If you encounter issues:

1. Check the troubleshooting section above
2. Search existing GitHub issues
3. Create a new issue with detailed information
4. Reach out to the development team

---

**Happy Coding! 🎉** 