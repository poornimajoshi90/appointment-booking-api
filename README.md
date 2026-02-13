# 🏥 Appointment Booking API

A production-ready ASP.NET Core Web API for managing appointment bookings with authentication, RBAC, Docker support, CI pipeline, and observability.

## 🚀 Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- BCrypt Password Hashing
- <img width="1905" height="1065" alt="Screenshot 2026-02-13 153209" src="https://github.com/user-attachments/assets/80c1571a-53d2-489f-9241-45e43c0fd6aa" />

- Docker & Docker Compose
- GitHub Actions CI
- Swagger (OpenAPI)
- <img width="1893" height="995" alt="Screenshot 2026-02-13 152210" src="https://github.com/user-attachments/assets/6f32a169-57d7-40ee-b9d6-4bacec5b1bde" />


- ## ✨ Features

- User Registration & Login
- JWT Authentication
- <img width="1887" height="1077" alt="Screenshot 2026-02-13 153353" src="https://github.com/user-attachments/assets/70578925-2671-4991-bfbc-b718c693731a" />
<img width="1797" height="931" alt="Screenshot 2026-02-13 152506" src="https://github.com/user-attachments/assets/e4978697-f2dc-42ae-8460-58c0ef777ebe" />

- Role-Based Access Control (Doctor/Patient)
- <img width="1756" height="849" alt="Screenshot 2026-02-13 153759" src="https://github.com/user-attachments/assets/e863ed1d-878b-4256-a682-84ce81f9cc90" />

- Appointment CRUD Operations
- Idempotency Support
- <img width="1796" height="854" alt="Screenshot 2026-02-13 152421" src="https://github.com/user-attachments/assets/9cecc0a6-bad4-4afc-a8a3-af0e60f1ec0a" />

- Rate Limiting
- Health Check Endpoint
- Structured Logging
- Dockerized Setup
- CI Pipeline

- ## 🏗 Architecture

The project follows Clean Architecture principles:

- Controllers → Handle HTTP Requests
- Services → Business Logic
- Repositories → Database Access
- DTOs → Data Transfer Objects
- Middleware → Logging & Exception Handling

Database: PostgreSQL
ORM: Entity Framework Core
Authentication: JWT Bearer

## ⚙️ Local Setup

### 1️⃣ Clone Repository

git clone https://github.com/poornimajoshi90/appointment-booking-api.git

cd appointment-booking-api

### 2️⃣ Run with Docker

docker-compose up --build

API runs at:
http://localhost:5000

Swagger:
http://localhost:5000/swagger

## 🔐 Environment Variables

- ConnectionStrings__DefaultConnection
- JWT__Key
- JWT__Issuer
- JWT__Audience

## 📖 API Documentation

Swagger UI available at:
/swagger

OpenAPI schema included.


## 🔄 CI Pipeline

GitHub Actions pipeline includes:

- Build
- Restore
- Run Tests
- Docker Build

## 📊 Observability

- Health Check Endpoint: /health
- Structured Logging
- Correlation ID support

## 🔐 Security Measures

- BCrypt Password Hashing
- JWT Expiry
- Role-Based Access Control
- Idempotency Keys
- Rate Limiting
- HTTPS Required
- Secrets via Environment Variables
- SQL Injection Protection (EF Core)
- OWASP Best Practices

<img width="1794" height="967" alt="Screenshot 2026-02-13 152338" src="https://github.com/user-attachments/assets/1e5b0647-34fe-401c-954b-31268e02e225" />






