# Design Doc

## Introduction
This is a ticketing web app. The core idea is that a user can create tickets with details and assign to users. Additional aspects like tagging and notifications are planned. 

## Goals
1. Create ticket
2. Update Status
3. Comments
4. View and filter tickers

## Data 
## Entities
- User
- Ticket
- Comment
- Board

## Relationships
- User => Tickets (creator, assigner)
- User => Comments
- Tickets => Comments
- Board => Tickets
- Ticket => Board
- User => Boards
- Board => User (owner)