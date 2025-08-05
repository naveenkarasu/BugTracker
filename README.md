
# Bug Tracker Lite

A comprehensive bug tracking application demonstrating enterprise-level development practices using .NET Core, Node.js, React, and modern DevOps tools.

## 🏗️ Architecture Overview

This project demonstrates a microservices architecture with the following components:

- **Frontend**: React SPA with TypeScript
- **Backend API**: ASP.NET Core with Identity and JWT authentication
- **Real-time Service**: Node.js Socket.IO for live updates
- **Database**: PostgreSQL for data persistence
- **Monitoring**: Prometheus for metrics collection
- **Visualization**: Grafana for metrics dashboard
- **Logging**: Serilog (.NET) and Winston (Node.js)

## 📁 Project Structure

```
BugTracker/
├── client/                 # React frontend application
├── server-dotnet/          # ASP.NET Core API with Identity
├── server-node/            # Node.js Socket.IO service
├── prometheus/             # Prometheus configuration
├── grafana/                # Grafana dashboards and configuration
├── docker-compose.yml      # Multi-service orchestration
├── .gitignore             # Git ignore rules
└── README.md              # This file
```

## 🚀 Quick Start

1. **Prerequisites**:
   - Docker and Docker Compose
   - .NET 8 SDK
   - Node.js 18+
   - Git

2. **Clone and Setup**:
   ```bash
   git clone <your-repo-url>
   cd BugTracker
   docker-compose up -d
   ```

3. **Access Services**:
   - Frontend: http://localhost:3000
   - .NET API: http://localhost:5000
   - Node.js Socket.IO: http://localhost:4000
   - Prometheus: http://localhost:9090
   - Grafana: http://localhost:3001

## 🔧 Development Setup

### Frontend (React)
```bash
cd client
npm install
npm start
```

### Backend API (.NET)
```bash
cd server-dotnet
dotnet restore
dotnet run
```

### Socket.IO Service (Node.js)
```bash
cd server-node
npm install
npm start
```

## 📊 Monitoring & Observability

- **Metrics**: Prometheus collects metrics from both .NET and Node.js services
- **Logging**: Structured logging with Serilog and Winston
- **Visualization**: Grafana dashboards for real-time monitoring
- **Health Checks**: Built-in health endpoints for all services

## 🔐 Authentication & Authorization

- **Identity**: ASP.NET Core Identity for user management
- **JWT**: JSON Web Tokens for stateless authentication
- **Roles**: Role-based access control (Admin, Developer, Tester, Viewer)

## 🐳 Docker Deployment

The entire application can be deployed using Docker Compose:

```bash
docker-compose up -d
```

This will start all services with proper networking and volume mounts.

## 📈 Learning Objectives

This project demonstrates:

1. **Microservices Architecture**: Separate services for different concerns
2. **Modern Authentication**: JWT-based auth with role management
3. **Real-time Communication**: WebSocket integration
4. **Observability**: Metrics, logging, and monitoring
5. **Containerization**: Docker-based deployment
6. **API Design**: RESTful APIs with proper documentation
7. **Frontend Development**: Modern React with TypeScript
8. **Database Design**: Relational database with proper relationships

## 🤝 Collaboration

This project is designed for collaborative development. Consider using:

- **GitHub/GitLab**: For source control and CI/CD
- **GitHub Issues**: For bug tracking and feature requests
- **Pull Requests**: For code review and collaboration
- **Branch Strategy**: Feature branches with main/master as stable

## 📚 Next Steps

1. Set up CI/CD pipelines
2. Add unit and integration tests
3. Implement advanced features (file uploads, notifications)
4. Add API documentation with Swagger
5. Set up development and staging environments

## 🆘 Support

For questions or issues, please create an issue in the repository or reach out to the development team. 

