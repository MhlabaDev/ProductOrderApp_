ProductOrderApp

Project Overview ProductOrderApp is a web-based application built using ASP.NET Core, C#, and SQL Server. It allows customers to register their personal details and place orders for a set of predefined products. This project demonstrates object-oriented programming concepts, database interactions, and frontend-backend integration. The project was created to showcase technical skills, including API development, database modeling, and user interface implementation.

Technologies Used 
Backend: ASP.NET Core 8, C#, Entity Framework Core 
Frontend: HTML, CSS, JavaScript, React (product-order-frontend folder)
Database: SQL Server / LocalDB Other: Bootstrap for styling

Features
Customer Registration Collects basic customer details including First Name, Surname, Address Type (Residential/Business), Street Address, Suburb, City/Town, and Postal Code. 
Customer details are stored in a SQL Server database. 
Product Listing Displays a list of 10 products with title, description, price, and image. 
Users can add products to their basket. Products are read-only; no CRUD operations are required for this section.

Order Management
Customers can add products to their basket and place orders. 
Allows customers to remove products from the basket.
Orders and order items are recorded in the database with correct quantities and pricing.

Database Management 
Includes EF Core models for Customer, Product, Order, and OrderItem. Configured decimal precision for product prices and unit prices.
Seeded database with 10 sample products.

Getting Started

Prerequisites 
.NET SDK 8
Node.js (for React frontend) 
SQL Server / LocalDB

Steps to Run
Clone the repository: git clone https://github.com/MhlabaDev/ProductOrderApp_.git 
Navigate to the backend folder: cd ProductOrderApi Restore dependencies and run the API: dotnet restore and dotnet run
Navigate to the frontend folder: cd ../product-order-frontend Install frontend dependencies: npm install 
Run the frontend: npm start

Author

Mhlaba Mkhatshane GitHub: https://github.com/MhlabaDev
