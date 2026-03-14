# Introduction

## Overview

The Appointment Booking API is a RESTful web service designed to manage medical appointment workflows between users and doctors.
It allows users to register, authenticate, book appointments, and interact with doctors through consultations and prescriptions.

The API is built using **ASP.NET Core 8** and follows modern backend development practices such as Clean Architecture, JWT authentication, role-based authorization, and containerized deployment.

---

## Purpose of the API

The main goal of this API is to provide a secure and scalable backend system for appointment scheduling in healthcare platforms.

Developers can use this API to:

- Register and authenticate users
- Manage appointments
- Handle doctor consultations
- Generate prescriptions
- Secure endpoints using authentication and authorization

---

## Key Features

- User registration and login with JWT authentication
- Role-based access control (User and Doctor roles)
- Appointment booking and management
- Consultation workflow for doctors
- Prescription creation
- Idempotency support for safe API retries
- Rate limiting for API protection
- Health check endpoints for monitoring
- Dockerized deployment and CI pipeline

---

## Technology Stack

The API is built using the following technologies:

- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Docker & Docker Compose
- GitHub Actions CI
- Swagger (OpenAPI)

---

## Base URL

The base URL for the API is:

http://localhost:8080/api

---

## API Documentation Structure

The complete API documentation is divided into multiple sections:

- API Endpoints
- Authentication Guide
- Error Codes
- Architecture Overview

These documents help developers understand how to integrate and interact with the API effectively.
