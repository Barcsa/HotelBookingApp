This project is my solution for the Octavic ASP.NET MVC test assignment. 
The goal was to build a simple hotel room booking application using ASP.NET MVC, SQL Server, and basic front-end tools.
The project allows creating users and rooms, managing bookings, and showing a calendar view for each room.

How it works

1. Users
Users can be created with basic details and an “IsAdmin” flag.
Login is very simple: the user just selects their name from a dropdown, and the session stores their ID and admin status.

2. Rooms
Rooms have a name, capacity, and optional description.
Each room also has a “View Calendar” page where all bookings for that room are displayed.

3. Bookings
A booking is linked to a user and a room, and has a start and end date.
The system checks two things before saving a booking:
	1. The end date must be after the start date.
	2. The room must not be booked in that time interval.

Admins see all bookings, while normal users only see their own.

4. Running the project

    1. git clone https://github.com/Barcsa/HotelBookingApp
	2. Set up SQL Server Express
	3. Update the connection string in appsettings.json if needed.
	4. Create the database using EF Core migrations: dotnet ef database update
	5. Run the application
