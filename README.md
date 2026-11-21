# Car Rental Management System
C# Windows Forms (.NET 10), MySQL, MySql.Data. MVC-style separation: Models/repositories, Controllers, Views, Data.

## Setup
1. Start your local MySQL server (for example through XAMPP).
2. Open phpMyAdmin at http://localhost/phpmyadmin, choose SQL, and run `database/car_rental_db.sql`.
3. Open `Data/DatabaseConfig.cs` and edit the connection string if your MySQL username/password/port differ. The default assumes localhost, database car_rental_db, user root, blank password.
4. Open `CarRentalSystem.csproj` in Visual Studio. Restore NuGet packages, build, and press F5.
5. Add customers and cars before creating bookings. Create bookings before recording payments.

## Features
Customers and cars: add, view, update, delete. Bookings: create, view, update status, delete. Payments: record, view, update status, delete.


