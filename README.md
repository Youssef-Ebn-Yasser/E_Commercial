E-Commerce Project (ASP.NET MVC with N-Tier Architecture)

📌 Overview

This is an E-Commerce System built using ASP.NET MVC with an N-Tier Architecture, comprising the following layers:

Presentation Layer (MVC Views & Controllers)

Business Layer (Service & Business Logic)

Data Layer (Repositories & Entity Framework Core)

The system enables customers to browse products, add them to the cart, and place orders. Admins have additional privileges to manage products, categories, category types, and users.

📁 Project Structure

🔹 Layers

Data Layer (DataLayer)

Contains database entities and relationships.

Uses Fluent Validation for constraints.

Implements Generic Repository & Unit of Work patterns.

Business Layer (BusinessLayer)

Contains business logic and application services.

Handles product, category, and order processing.

Presentation Layer (PresentationLayer)

Implements MVC Views & Controllers.

Uses AutoMapper for ViewModel transformation.

Includes Helpers & UI Components.

🛠️ Features

🔹 For Clients

🛒 Browse Products & Categories

➕ Add Products to Cart

🔐 Register & Login

🛍️ Place Orders

🔹 For Admins

👥 Manage Users

🛠️ CRUD Operations:

✅ Products

✅ Categories

✅ Category Types

🔹 System Features

🏷️ Only Admins can access restricted features.

📢 Advertisements System Integration.

📱 Fully Responsive UI with Bootstrap.

🏗️ Database Schema

🔹 Tables & Relationships

Product

ProductId (PK)

ProductName

Details

Brand

Price

AmountInStock

CreatedDate

Each product has multiple images.

Category

CategoryId (PK)

CategoryName

CreatedDate

Each category can have multiple products.

Category Type

CategoryTypeId (PK)

CategoryTypeName

Each category type can have multiple categories.

Client

ClientId (PK)

Name

Username

Password

Address

Phone

Email

Type (default: Client)

Orders & Product Orders

OrderId (PK)

ClientId (FK)

TotalPrice

Discount

AfterDiscount

CreatedAt

Each order contains multiple products.

📚 Technologies Used

ASP.NET Core MVC

Entity Framework Core

Fluent Validation (Data Constraints)

Generic Repository & Unit of Work

AutoMapper (DTO Mapping)

Bootstrap 5 (Responsive UI)

CORS (Cross-Origin Resource Sharing)

Identity Authentication & Authorization (For Admin System)

JSON & Cookies (Shopping Cart Persistence)
