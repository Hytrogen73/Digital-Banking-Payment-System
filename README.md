# Digital Banking & Payment System

## Project Overview

The Digital Banking & Payment System is a simulated digital banking application developed as my Software Development graduation project at IT Academy STEP Cambodia.

The application aims to allow users to manage their accounts, view balances, transfer money between registered users, and track their financial activities.

**Note:** This application is for educational purposes only. It will simulate financial transactions and will not process real money.

## Planned Features

- User registration and login
- Account information and balance management
- Money transfers between registered users
- Money requests with accept and reject options
- Transaction history
- Income and expense tracking
- Saved contacts
- Input validation and account security

## Technologies

**Android Application**
- Kotlin
- Android Studio
- Retrofit

**Backend**
- C#
- ASP.NET Core Web API
- Entity Framework Core

**Database**
- Microsoft SQL Server

**Security**
- JWT authentication
- Password hashing
- HTTPS

**Version Control**
- Git and GitHub

## Project Status

Currently in development.

### Development Progress

**Completed:**
- Set up ASP.NET Core Web API.
- Implemented Account and User models with validation.
- Implemented password hashing using ASP.NET Core PasswordHasher.
- Configured Dependency Injection.
- Added unit tests for Account and User models.
- Installed and configured Entity Framework Core with SQL Server.
- Created AppDbContext for Users and Accounts.
- Created the InitialCreate migration.
- Successfully created DigitalBankingDB with Users and Accounts tables.
- Connected API controllers to SQL Server using Entity Framework Core.
- Implemented an API endpoint to retrieve all accounts.
- Implemented an API endpoint to retrieve an account by ID.
- Implemented an API endpoint to create a new account.
- Tested successful account creation and account retrieval, including the 404 Not Found response for an account that does not exist.

**Current Stage:**
- Developing database operations through ASP.NET Core API controllers.
- Building the foundation for account management using Entity Framework Core.

**Next Steps:**
- Implement account balance update operations.
- Improve input validation and error handling.
- Develop user registration and login endpoints.
- Implement JWT authentication and authorization.
- Continue developing banking features.

## Project Purpose

This project aims to demonstrate mobile application development, backend API development, database management, authentication, and secure transaction processing.