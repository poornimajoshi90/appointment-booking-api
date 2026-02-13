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

## 🏗 Clean Architecture Overview

```mermaid
flowchart LR

    Client --> Controller
    Controller --> Service
    Service --> Repository
    Repository --> Database[(PostgreSQL)]

    Controller --> Middleware
    Middleware --> Logging
    Middleware --> ExceptionHandling
```


## 🗄️ Entity Relationship Diagram


```mermaid
erDiagram

    USER ||--o{ APPOINTMENT : books
    USER ||--o{ IDEMPOTENCY_KEY : generates

    APPOINTMENT ||--|| CONSULTATION : has
    CONSULTATION ||--o{ PRESCRIPTION : includes

    USER {
        int Id PK
        string FullName
        string Email
        string PasswordHash
        string Role
        datetime CreatedAt
    }

    APPOINTMENT {
        int Id PK
        datetime AppointmentDate
        string Status
        string Notes
        int UserId FK
        datetime CreatedAt
    }

    CONSULTATION {
        int Id PK
        string Diagnosis
        string DoctorNotes
        int AppointmentId FK
        datetime CreatedAt
    }

    PRESCRIPTION {
        int Id PK
        string MedicineName
        string Dosage
        string Duration
        int ConsultationId FK
        datetime CreatedAt
    }

    IDEMPOTENCY_KEY {
        int Id PK
        string Key
        string RequestHash
        datetime CreatedAt
        int UserId FK
    }
```


## 🔄 Application Flow

```mermaid
flowchart TD

    A[User Registers/Login] --> B[Generate JWT]
    B --> C[Access Protected API]

    C --> D[Book Appointment]
    D --> E[Store Appointment in DB]

    E --> F[Doctor Creates Consultation]
    F --> G[Add Diagnosis & Notes]

    G --> H[Generate Prescription]
    H --> I[Store Medicines]

    C --> J[Idempotency Middleware]
    C --> K[JWT Validation]
    C --> L[RBAC Authorization]

    I --> M[(PostgreSQL Database)]
```




## ⚙️ Local Setup

### 1️⃣ Clone Repository

git clone https://github.com/poornimajoshi90/appointment-booking-api.git

cd appointment-booking-api

## 🚀 API Running Information

API runs at:

http://localhost:8080

Swagger UI:

http://localhost:8080/swagger/index.html


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






