# Payment-Portal
This project is a simple Payments Portal built as part of a full-stack technical assignment.

Users can:
- View payments
- Add new payments
- Edit existing payments
- Delete payments

Duplicate transactions are prevented using a unique clientRequestId.

---

## Tech Stack

### Frontend
- Angular (Standalone Components)
- TypeScript
- Reactive Forms
- HTTP Client

### Backend
- ASP.NET Core 8 Web API
- Entity Framework Core
- In-Memory Database

---

## Project Structure
payment-portal/
│
├── api/ → ASP.NET Core 8 Web API
├── ui/ → Angular application (standalone components)
├── screenshots/
└── README.md


## Database
- In-memory database used for faster setup
- DB schema attached as markdown/screenshot
- Relational structure can be easily migrated to SQL Server
