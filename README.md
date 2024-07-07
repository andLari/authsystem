# Auth System WPF

This is a WPF application for authorization and registration. It allows you to view data from the database and create, view, and edit requests.

## Features
- User Authorization
- User Registration
- View Data from Database
- Create, View, and Edit Requests

## Installation Guide

### Setting Up the Database
1. **Install SQL Server Studio**: Ensure that you have SQL Server Studio installed on your machine.
2. **Import Database**: Drag and drop the `mybase.sql` file into SQL Server Studio to create the necessary database.
3. **Configure Connection String**: Open `databaseconfig.cs` and edit the connection string to match your database setup.

```csharp
// Example connection string configuration
public static string sqlstring = @"Data Source = Type your database address; Initial Catalog = db10; Integrated Security = True";
