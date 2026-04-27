# Tech Support System

A simple ticket management system built with ASP.NET Core and Entity Framework Core.

## Features
- Create and manage support tickets
- Assign technicians to tickets (many-to-many relationship)
- Add notes to tickets
- Update ticket status (Open/Closed)
- Layered architecture (Controller, Service, Repository)

## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- Sqlite (SQL Server in the future)
- xUnit (unit testing)
- Moq (mocking)

## Database
Includes the following tables:
- Ticket
- Technician
- Note
- TicketTechnician (junction table)

## Testing
Unit tests are included for the service layer using xUnit and Moq.
