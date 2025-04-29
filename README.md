# Schiessmeister

## Project Background

Schiessmeister is a specialized shooting competition management system developed to address the growing needs of shooting clubs and competition organizers. The project emerged from the recognition that many shooting clubs still rely on manual processes or outdated systems for managing their competitions and members. This digital transformation initiative aims to modernize the shooting sports administration by providing a comprehensive, user-friendly solution that streamlines the entire competition lifecycle.

The system is designed to serve multiple stakeholders:

-   Competition organizers who need efficient tools for event management
-   Shooting clubs looking to digitize their administrative processes
-   Participants who want easy access to competition information and results
-   Club administrators who need comprehensive member and equipment management

## Overview

Schiessmeister is a comprehensive shooting competition management system designed to streamline the organization and documentation of shooting events. The system provides organizers with an efficient and intuitive tool to manage competitions from creation to live monitoring. The solution integrates modern backend and frontend technologies to ensure both stable data processing and an engaging user experience.

## Core Use Cases

### Competition Management

-   Create and manage shooting competitions
-   Real-time competition monitoring and control
-   Live result display and tracking
-   Comprehensive competition overview dashboard

### Participant Management

-   Add and manage participants for each competition
-   Assign points to participants
-   Track participant performance and statistics

### User Management

-   Secure registration and login system
-   Role-based access control (organizers, participants)
-   Identity management and authentication

## Features

-   Member management and authentication
-   Competition organization and result tracking
-   Shooting range scheduling and booking
-   Equipment inventory management
-   Performance statistics and reporting
-   User role management (admin, member, guest)
-   Real-time updates via WebSocket/SignalR
-   Live competition monitoring

## Backend Technology Stack

-   ASP.NET Core 8.0
-   Entity Framework Core
-   SQL Server
-   RESTful API architecture
-   JWT Authentication
-   Identity Framework
-   WebSocket/SignalR for real-time communication

# Schiessmeister Client

## Features

-   Responsive dashboard interface
-   Real-time competition updates
-   Interactive shooting range booking calendar
-   Member profile management
-   Performance statistics visualization
-   Mobile-friendly design
-   Live competition monitoring interface
-   Intuitive competition management tools

## Frontend Technology Stack

-   React 18
-   Vite
-   TypeScript
-   Modern CSS (Tailwind/styled-components)
-   REST API integration
-   WebSocket/SignalR client integration

# Technical Implementation Details

### Backend Architecture

The backend architecture is built on .NET Core and utilizes Entity Framework Core (EFCore) for database access and management. A REST API, developed with WebAPI, provides the necessary endpoints for data communication. The Identity Framework is implemented to meet security requirements for authentication and authorization.

### Frontend Architecture

The frontend provides a comprehensive interface that covers all relevant use cases. Organizers can view their competitions, create new events, manage participants, and track live results through an intuitive interface.

### Real-time Communication

The application implements two-way communication (WebSocket/SignalR) to enable dynamic and interactive usage. This ensures that changes such as point entries or new participants are immediately reflected in the live result display.

All rights reserved © Schiessmeister 2025
