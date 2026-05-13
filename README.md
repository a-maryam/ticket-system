# Ticketing System
In-progress ticketing web app with Jira-like functionality

## [Design doc](./Design.md)

## Demo: Ticket API in Postman 
![Ticket API](./demo_images/create_ticket_request.png)

## Wireframe (Will be writing react components soon)
![Basic UI Wireframe](./wireframe.png)

## Tech
- ASP.NET Core 10, C#, Entity Framework Core + Fluid API, MS SQL Server + Postman for Local Testing
- Frontend: React, Vite, Javascript
- Data: Either Azure SQL Database or Supabase dependent on host
- Hosting: Between Railway, Vercel, and Azure 
- Auth: Likely JWT Auth

## Dev Log
- Wrote models for ticket, user, board, comment, with foreign keys
- Wrote TicketController, Ticket Dto, AppDbContext
- Added AppDbContextFactory so Entity Framework can connect with the MS SQL Server for local testing
- create ticket api, controllers and service set up. 
- fixed cascading deletes 
- wrote board creation logic for creating new ticket (either existing board or new one)
- wrote ticket + board dtos
- added dummy user to appdbcontext for flow to work initially
- assign ticket endpoint works
- wrote get, create board, works in conjunction with create ticket + assign ticket
- wrote change ticket status endpoint
- wrote column model
- adjusted ticket service for column 
- adjusted board service for column
- columnid in ticket flow fixed
- started column service/controller
- wrote ticket delete, ticket update
- wrote column service
- wrote move ticket
- added get boards for owner
- starting react + javascript app

## Planned/Ideas
- for UX, add a flow where a ticket can be created concurrently with ticket (had to remove option when adding column to flow)

## Design thoughts
- Initially tried a design with column name and board id to look up columns in ticket creation, but it would cause problems down the line. Changed to using columnid. 
- Thought that I needed multiple cases for move ticket; actually, condensed into one. Sometimes it means there is extra repositioning, but it is worth the simpler design. There's also some room for optimization here in terms of how much we load.
