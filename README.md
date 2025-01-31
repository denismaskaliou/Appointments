# 📌 Appointments API
> REST API to retrieve available appointment slots with sales managers.

## 📖 Overview
This project provides a **Web API** built with **ASP.NET Core 8** and **PostgreSQL** for managing appointment slots.  
It allows customers to book available slots based on sales manager availability and filtering criteria.

## 🚀 Technologies Used
- **ASP.NET Core 8**
- **Entity Framework Core**
- **PostgreSQL**
- **Docker & Docker Compose**

---

## 🛠 How to Run
### 1️⃣ Install Required Dependencies
Before running the project, ensure you have:
- **Docker** and **Docker Compose**
- **.NET SDK 8.0** (if running outside Docker)

### 2️⃣ Start the Project with Docker
Run the following command:

```sh
docker-compose up --build
```

✅ This will start both **PostgreSQL** and **Web API** inside Docker containers.

---

## 📌 Project Structure
```
/Appointments
│── /src
│   ├── /Appointments.WebAPI            
│   │   ├── /Application
│   │   │   ├── /Services
│   │   │   ├── /Validators
│   │   ├── /Database
│   │   │   ├── /Configurations
│   │   │   ├── ApplicationDbContext.cs
│   │   ├── /Models
│   │   │   ├── /DTOs
│   │   │   ├── /Entities
│   │   ├── /Web
│   │   │   ├── /Controllers 
│   │   │   ├── /Converters
│   │   ├── appsettings.json
│   │   ├── Program.cs
│   │   ├── Startup.cs
|   ├── /Appointments.Tests
│── /database
│   ├── Dockerfile
│   ├── init.sql
│── docker-compose.yml
│── README.md