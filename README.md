# E-Commerce Management System

A backend-focused E-Commerce Management System built with **C#, .NET, Entity Framework Core, and SQL Server**.

The main purpose of this project is to practice and demonstrate Entity Framework Core concepts ranging from basic entity relationships and database configuration to querying, performance optimization, inheritance, database views, interceptors, and soft deletion.

---

## Technologies

- C#
- .NET
- Entity Framework Core
- SQL Server
- LINQ

---

## Project Overview

The system represents a simplified e-commerce platform containing:

- Users
- Customers
- Employees
- Admins
- Products
- Categories
- Orders
- Order Items
- Customer Profiles
- Reviews
- Product Sales Summary

The project focuses on database modeling and data access using **Entity Framework Core**.

---

# Domain Model

## User Hierarchy

The project uses **Table-Per-Hierarchy (TPH)** inheritance.

```text
User
├── Customer
└── Employee
    └── Admin