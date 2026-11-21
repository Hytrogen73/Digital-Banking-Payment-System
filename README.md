# Digital Banking & Payment System

## Project Overview

The Digital Banking & Payment System is a simulated digital banking application developed as my Software Development graduation project at IT Academy STEP Cambodia.

The application aims to allow users to manage their accounts, view balances, transfer money between registered users, and track their financial activities.

**Note:** This application is for educational purposes only. It simulates financial transactions and does not process real money.

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

### Android Application
- Kotlin
- Android Studio
- Retrofit

### Backend
- C#
- ASP.NET Core Web API
- Entity Framework Core

### Database
- Microsoft SQL Server

### Security
- JWT authentication
- Password hashing
- HTTPS
- Refresh token hashing and rotation

### Version Control
- Git and GitHub

## Project Status

Currently in development.

### Completed Features

#### Backend and Database
- ASP.NET Core Web API setup.
- SQL Server integration with Entity Framework Core.
- User and Account models with validation.
- Account creation and retrieval.
- Deposit and withdrawal operations.
- Money transfers between accounts.
- Database transactions with Commit and Rollback.
- RowVersion concurrency control.
- Transaction history with pagination.
- Account ownership protection.
- Account operations refactored into AccountService.

#### Authentication and Security
- User registration endpoint.
- User login endpoint.
- Password hashing using ASP.NET Core PasswordHasher.
- JWT generation and authentication.
- Authorization and account ownership protection.
- Secure refresh token generation and database storage.
- SHA-256 refresh token hashing.
- Refresh token validation and expiration.
- Refresh token rotation and revocation.
- Logout and refresh token revocation.
- Refresh token reuse detection.

#### Testing
- Unit tests for account operations and validation.
- Authentication and refresh token testing.
- Transaction history and transfer testing.
- HTTP endpoint testing using `.http` files.
- Successful build and test execution.

**Latest test results:**
- Total tests: 12
- Passed: 12
- Failed: 0
- Skipped: 0

### Current Stage

- Backend account management and transaction foundations completed.
- JWT authentication and refresh token session management completed.
- AccountService refactoring completed.
- Backend HTTP testing completed for the tested account operations.
- Preparing for further backend architecture improvements.

### Planned Features

#### Backend
- Money requests with accept and reject options.
- Income and expense tracking.
- Saved contacts.
- Further improvements to transfer reliability and duplicate transaction protection.
- Additional service interfaces and architecture improvements.

#### Android Application
- Develop the Android application using Kotlin.
- Build registration and login screens.
- Implement account dashboard and balance display.
- Integrate the Android application with the backend API using Retrofit.
- Implement money transfers and transaction history.

## Project Purpose

This project demonstrates mobile application development, backend API development, database management, authentication, authorization, and secure transaction processing.

It provides practical experience in developing a full-stack mobile banking application using modern software development technologies.