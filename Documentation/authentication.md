# Authentication

The Appointment Booking API uses **JWT (JSON Web Token)** based authentication to secure protected endpoints.

After a successful login, the server returns a JWT token which must be included in subsequent API requests.

---

## Login to Get Token

Endpoint

POST /api/users/login

Example Request

```json
{
  "email": "poornima@gmail.com",
  "password": "Password@123"
}
```

Example Response

```json
{
  "success": true,
  "token": "JWT_TOKEN"
}
```

---

## Using the Token

Include the token in the **Authorization header** for protected APIs.

Example:

Authorization: Bearer JWT_TOKEN

---

## Role Based Authorization

The API supports **Role-Based Access Control (RBAC)**.

Available roles:

User
Doctor

Example:

Doctor-only APIs:

GET /api/appointments
PUT /api/appointments/{id}/approve
PUT /api/appointments/{id}/reject
