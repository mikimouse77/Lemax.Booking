# Lemax.Booking

Lemax.Booking is a hotel search application designed to find hotels based on distance and price. The application consists of two main API services:

- **Lemax.Booking.HotelManagement.API**: Manages hotel data.
- **Lemax.Booking.Search.API**: Handles hotel search functionality.

## Overview

The application is containerized using Docker and managed with Docker Compose. It includes the following components:

- **Hotel Management API**: Exposed on port `5001`.
- **Search API**: Exposed on port `5178`.
- **SQL Server**: Database instance exposed on port `1434`.
- **Elasticsearch**: For searching hotels based on distance and price, exposed on ports `9200` and `9300`.

## Setup and Installation

### Prerequisites

- Docker
- Docker Compose

### Running the Application

1. **Clone the repository**:

   git clone <repository-url>
   cd lemax.booking

2. **Start the application using Docker Compose**:

   docker-compose up

   This command will start the following services:
   - `Lemax.Booking.HotelManagement.API` on port `5001`
   - `Lemax.Booking.Search.API` on port `5178`
   - SQL Server on port `1434`
   - Elasticsearch on ports `9200` and `9300`

3. **Ensure the following ports are available**:
   - `5001` for Hotel Management API
   - `5178` for Search API
   - `1434` for SQL Server
   - `9200` and `9300` for Elasticsearch

### Configuration

- **Hotel Management API**: Manages CRUD operations for hotel data.
- **Search API**: Provides search functionality based on distance and price using Elasticsearch.

## How It Works

- **Hotel Management API**: Handles operations related to hotels such as adding, updating, and retrieving hotel information.
- **Search API**: Queries Elasticsearch to find hotels based on distance and price. The Elasticsearch index includes hotel information along with geospatial data for distance calculations.

## Communication

The two API services communicate via HTTP protocol.

## Troubleshooting

- **Service Not Starting**: Ensure all required ports are available and no other services are using them.
- **Elasticsearch Connection Issues**: Verify that Elasticsearch is running and accessible on ports `9200` and `9300`.
