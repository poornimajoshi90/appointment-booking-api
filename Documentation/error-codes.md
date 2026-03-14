# API Error Codes

This section describes the common error responses returned by the Appointment Booking API.

---

## Standard HTTP Status Codes

| Status Code | Meaning                                   |
| ----------- | ----------------------------------------- |
| 200         | Request successful                        |
| 400         | Bad request (Invalid input)               |
| 401         | Unauthorized (Authentication required)    |
| 403         | Forbidden (User does not have permission) |
| 404         | Resource not found                        |
| 500         | Internal server error                     |

---

## Example Error Response

```json
{
  "success": false,
  "message": "Invalid request data"
}
```

---

## Authentication Errors

### Missing Token

If the Authorization header is missing:

```json
{
  "status": 401,
  "message": "Unauthorized"
}
```

---

### Invalid Token

If the JWT token is invalid or expired:

```json
{
  "status": 401,
  "message": "Invalid or expired token"
}
```

---

## Idempotency Errors

If the **Idempotency-Key** header is missing while creating an appointment:

```json
{
  "status": 400,
  "message": "Idempotency-Key header required"
}
```
