#!/bin/bash

# Bug Tracker Lite - Development Startup Script

echo "🚀 Starting Bug Tracker Lite in development mode..."

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker and try again."
    exit 1
fi

# Check if Docker Compose is available
if ! command -v docker-compose &> /dev/null; then
    echo "❌ Docker Compose is not installed. Please install Docker Compose and try again."
    exit 1
fi

echo "📦 Building and starting services..."

# Build and start all services
docker-compose up --build -d

# Wait for services to be ready
echo "⏳ Waiting for services to be ready..."
sleep 30

# Check service health
echo "🔍 Checking service health..."

# Check .NET API
if curl -f http://localhost:5000/health > /dev/null 2>&1; then
    echo "✅ .NET API is running at http://localhost:5000"
else
    echo "❌ .NET API is not responding"
fi

# Check Node.js Socket.IO service
if curl -f http://localhost:4000/health > /dev/null 2>&1; then
    echo "✅ Socket.IO service is running at http://localhost:4000"
else
    echo "❌ Socket.IO service is not responding"
fi

# Check React frontend
if curl -f http://localhost:3000 > /dev/null 2>&1; then
    echo "✅ React frontend is running at http://localhost:3000"
else
    echo "❌ React frontend is not responding"
fi

# Check Prometheus
if curl -f http://localhost:9090 > /dev/null 2>&1; then
    echo "✅ Prometheus is running at http://localhost:9090"
else
    echo "❌ Prometheus is not responding"
fi

# Check Grafana
if curl -f http://localhost:3001 > /dev/null 2>&1; then
    echo "✅ Grafana is running at http://localhost:3001"
else
    echo "❌ Grafana is not responding"
fi

echo ""
echo "🎉 Bug Tracker Lite is ready!"
echo ""
echo "📱 Access the application:"
echo "   Frontend:     http://localhost:3000"
echo "   .NET API:     http://localhost:5000"
echo "   Socket.IO:    http://localhost:4000"
echo "   Prometheus:   http://localhost:9090"
echo "   Grafana:      http://localhost:3001 (admin/admin)"
echo ""
echo "🔧 Development commands:"
echo "   View logs:    docker-compose logs -f"
echo "   Stop services: docker-compose down"
echo "   Restart:      docker-compose restart"
echo ""
echo "📊 Default users:"
echo "   Admin:        admin@bugtracker.com / Admin123!"
echo "   Developer:    developer@bugtracker.com / Dev123!"
echo "   Tester:       tester@bugtracker.com / Test123!"
echo "   Viewer:       viewer@bugtracker.com / View123!"
echo "" 