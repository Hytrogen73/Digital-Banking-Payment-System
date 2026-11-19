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

### Completed Features

**Backend and Database**
- ASP.NET Core Web API setup.
- SQL Server integration with Entity Framework Core.
- User and Account models with validation.
- Account creation, retrieval, deposit, and withdrawal endpoints.
- Money transfers between accounts.
- Database transactions with Commit and Rollback.
- RowVersion concurrency control.

**Authentication and Security**
- Password hashing using ASP.NET Core PasswordHasher.
- JWT login and authentication.
- Authorization and account ownership protection.
- Secure refresh token generation and database storage.
- SHA-256 refresh token hashing.
- Refresh token validation, expiration, revocation, and rotation.

**Testing**
- Unit tests for account operations and validation.
- Successfully tested authentication and refresh token rotation.
- All 9 unit tests passing.

### Current Stage

- Backend account management and transfer foundations completed.
- JWT authentication and refresh token rotation completed.
- Preparing to implement logout and session management.

### Planned Features

**Authentication**
- User registration endpoint.
- Logout and refresh token revocation.

**Banking Features**
- Money requests with accept and reject options.
- Transaction history.
- Income and expense tracking.
- Saved contacts.
- Improved transfer reliability and duplicate transaction protection.

**Android Application**
- Develop the Android application using Kotlin.
- Integrate the Android application with the backend API using Retrofit.

## Project Purpose

This project demonstrates mobile application development, backend API development, database management, authentication, and secure transaction processing.