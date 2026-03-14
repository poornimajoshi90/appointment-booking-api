# Appointment Booking API Endpoints

Base URL

http://localhost:8080/api

All protected APIs require a **JWT token** in the Authorization header.

Example:

Authorization: Bearer YOUR_JWT_TOKEN

---

# 1. User APIs

## Register User

**Endpoint**

POST /api/users/register

**Description**

Registers a new user in the system.

**Request Body**

```json
{
  "fullName": "Poornima Joshi",
  "email": "poornima@gmail.com",
  "password": "Password@123",
  "role": "User"
}
```

**Response**

```json
{
  "success": true,
  "message": "User Registered Successfully"
}
```

---

## Login User

**Endpoint**

POST /api/users/login

**Description**

Authenticates the user and returns a JWT token.

**Request Body**

```json
{
  "email": "poornima@gmail.com",
  "password": "Password@123"
}
```

**Response**

```json
{
  "success": true,
  "token": "JWT_TOKEN"
}
```

---

## Get Profile

**Endpoint**

GET /api/users/profile

**Authorization**

Required

**Response**

```json
"You are authenticated!"
```

---

## Doctor Dashboard

**Endpoint**

GET /api/users/doctor-dashboard

**Authorization**

Required (Doctor Role)

**Response**

```json
"Welcome Doctor!"
```

---

# 2. Appointment APIs

## Create Appointment

**Endpoint**

POST /api/appointments

**Authorization**

Required

**Headers**

Idempotency-Key: UNIQUE_REQUEST_KEY

**Description**

Creates a new appointment for the authenticated user.

**Request Body**

```json
{
  "appointmentDate": "2026-04-25T10:30:00",
  "notes": "General consultation"
}
```

**Response**

```json
{
  "success": true,
  "data": "Appointment created successfully"
}
```

---

## Get My Appointments

**Endpoint**

GET /api/appointments/my-appointments

**Authorization**

Required (User / Doctor)

**Response**

```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "appointmentDate": "2026-04-25T10:30:00",
      "status": "Pending"
    }
  ]
}
```

---

## Get All Appointments

**Endpoint**

GET /api/appointments

**Authorization**

Required (Doctor Role)

**Response**

```json
{
  "success": true,
  "data": []
}
```

---

## Approve Appointment

**Endpoint**

PUT /api/appointments/{id}/approve

Example:

PUT /api/appointments/5/approve?doctorId=2

**Authorization**

Required (Doctor Role)

**Response**

```json
{
  "success": true,
  "message": "Appointment approved successfully"
}
```

---

## Reject Appointment

**Endpoint**

PUT /api/appointments/{id}/reject

Example:

PUT /api/appointments/5/reject

**Authorization**

Required (Doctor Role)

**Response**

```json
{
  "success": true,
  "message": "Appointment rejected successfully"
}
```

---

# 3. Consultation APIs

## Start Consultation

**Endpoint**

PUT /api/consultations/{id}/start

Example:

PUT /api/consultations/10/start

**Authorization**

Required (Doctor Role)

**Response**

```json
{
  "success": true,
  "message": "Consultation started successfully"
}
```

---

## Complete Consultation

**Endpoint**

PUT /api/consultations/{id}/complete

Example:

PUT /api/consultations/10/complete

**Authorization**

Required (Doctor Role)

**Response**

```json
{
  "success": true,
  "message": "Consultation completed successfully"
}
```

---

## Get My Consultations

**Endpoint**

GET /api/consultations/my-consultations

**Authorization**

Required (Doctor Role)

**Response**

```json
{
  "success": true,
  "data": []
}
```

---

# 4. Prescription APIs

## Create Prescription

**Endpoint**

POST /api/prescriptions

**Authorization**

Required (Doctor Role)

**Request Body**

```json
{
  "medicineName": "Paracetamol",
  "dosage": "500mg",
  "duration": "5 days",
  "consultationId": 10
}
```

**Response**

```json
{
  "success": true,
  "message": "Prescription Created Successfully"
}
```
