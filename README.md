# Ticketing System
In-progress ticketing web app with Jira-like functionality

## [Design doc](./Design.md)

## Tech
.NET 10, ASP.NET Core, C#, Entity Framework Core, MS SQL Server for Local Testing

## Dev Log
- Wrote models for ticket, user, board, comment, with foreign keys
- Wrote TicketController, Ticket Dto, AppDbContext
- Added AppDbContextFactory so Entity Framework can connect with the MS SQL Server for local testing
- create ticket api, controllers and service set up. 
- fixed cascading deletes 
- Next: Edit dtos / get EF all the way working