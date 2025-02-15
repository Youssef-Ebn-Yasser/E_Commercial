# E-Commerce Project (ASP.NET MVC with N-Tier Architecture)

## 📌 Overview
This is an **E-Commerce System** built using **ASP.NET MVC** with an **N-Tier Architecture**, comprising the following layers:

- **Presentation Layer** (MVC Views & Controllers)
- **Business Layer** (Service & Business Logic)
- **Data Layer** (Repositories & Entity Framework Core)

The system enables **customers** to browse products, add them to the cart, and place orders. **Admins** have additional privileges to manage products, categories, category types, and users.

---

## 📁 Project Structure
### 🔹 **Layers**
1. **Data Layer** (`DataLayer`)
   - Contains database entities and relationships.
   - Uses **Fluent Validation** for constraints.
   - Implements **Generic Repository** & **Unit of Work** patterns.

2. **Business Layer** (`BusinessLayer`)
   - Contains business logic and application services.
   - Handles product, category, and order processing.

3. **Presentation Layer** (`PresentationLayer`)
   - Implements **MVC Views & Controllers**.
   - Uses **AutoMapper** for ViewModel transformation.
   - Includes **Helpers & UI Components**.

---

## 🛠️ Features
### 🔹 **For Clients**
- 🛒 Browse Products & Categories
- ➕ Add Products to Cart
- 🔐 Register & Login
- 🛍️ Place Orders

### 🔹 **For Admins**
- 👥 Manage Users
- 🛠️ CRUD Operations:
  - ✅ **Products**
  - ✅ **Categories**
  - ✅ **Category Types**

### 🔹 **System Features**
- 🏷️ Only Admins can access restricted features.
- 📢 Advertisements System Integration.
- 📱 Fully **Responsive UI** with Bootstrap.

---

## 🏗️ Database Schema
### 🔹 **Tables & Relationships**
1. **Product**
   - `ProductId` (PK)
   - `ProductName`
   - `Details`
   - `Brand`
   - `Price`
   - `AmountInStock`
   - `CreatedDate`
   - **Each product has multiple images**.

2. **Category**
   - `CategoryId` (PK)
   - `CategoryName`
   - `CreatedDate`
   - **Each category can have multiple products**.

3. **Category Type**
   - `CategoryTypeId` (PK)
   - `CategoryTypeName`
   - **Each category type can have multiple categories**.

4. **Client**
   - `ClientId` (PK)
   - `Name`
   - `Username`
   - `Password`
   - `Address`
   - `Phone`
   - `Email`
   - `Type` (default: **Client**)

5. **Orders & Product Orders**
   - `OrderId` (PK)
   - `ClientId` (FK)
   - `TotalPrice`
   - `Discount`
   - `AfterDiscount`
   - `CreatedAt`
   - **Each order contains multiple products**.

---

## 📚 Technologies Used
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **Fluent Validation** (Data Constraints)
- **Generic Repository & Unit of Work**
- **AutoMapper** (DTO Mapping)
- **Bootstrap 5** (Responsive UI)
- **CORS** (Cross-Origin Resource Sharing)
- **Identity Authentication & Authorization** (For Admin System)
- **JSON & Cookies** (Shopping Cart Persistence)

---

## 🚀 How to Run the Project
1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-repo/ecommerce-mvc.git
   cd ecommerce-mvc
   ```
2. **Setup Database:**
   - Configure connection string in `appsettings.json`.
   - Run migrations:
     ```sh
     dotnet ef database update
     ```

3. **Run the application:**
   ```sh
   dotnet run
   ```
4. **Access the Web App:**
   - Client: `http://localhost:5000`
   - Admin: `http://localhost:5000/admin`

---

## 🏆 Future Enhancements
✅ Add Payment Gateway Integration (Stripe, PayPal)
✅ Implement Wishlist & Favorites
✅ Improve Admin Dashboard UX
✅ Add Email Notifications for Orders

---

**👨‍💻 Developed By: [Your Name]**  
📧 Contact: your.email@example.com

