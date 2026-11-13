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
- Implemented Deposit API endpoint with EF Core.
- Implemented Withdraw API endpoint with exception handling.
- Added account balance validation for deposit and withdrawal operations.
- Tested successful and invalid deposit and withdrawal requests.
- Expanded unit tests from 5 to 7, with all tests passing.
- Implemented Data Annotation validation using Range attributes.
- Added unit tests for account amount validation.
- Implemented Transfer API endpoint between accounts.
- Added sender and receiver account validation.
- Added protection against transferring to the same account.
- Implemented database transactions using Commit and Rollback.
- Added handling for insufficient balance and invalid transfer requests.
- Implemented RowVersion concurrency control using EF Core.
- Added DbUpdateConcurrencyException handling to prevent conflicting account updates.
- Created and applied the AddAccountRowVersion migration.
- Successfully tested concurrent database updates using two separate DbContext instances.
- Verified that outdated RowVersion values are rejected and that the database balance remains correct.
- Successfully ran all unit tests.

**Current Stage:**

- Completed foundational account management operations.
- Completed the initial Transfer API implementation.
- Implemented database transaction and concurrency protection.
- Preparing for authentication, authorization, and additional banking features.

**Next Steps:**

- Develop user registration and login endpoints.
- Implement JWT authentication and authorization.
- Enforce account ownership and access control.
- Improve transfer reliability and duplicate transaction protection.
- Develop money request functionality with accept and reject options.
- Implement transaction history.
- Develop income and expense tracking.
- Integrate the Android application with the backend API.

## Project Purpose

This project aims to demonstrate mobile application development, backend API development, database management, authentication, and secure transaction processing.