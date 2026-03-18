# Organization CRUD MVC Application

This is a complete ASP.NET Web MVC application designed to demonstrate Create, Read, Update, and Delete (CRUD) operations using Entity Framework Code-First and SQL Server.

## 📋 Features

*   **Department Management**: Add, view, edit, and delete company departments.
*   **User Management**: Add, view, edit, and delete users while assigning them to existing departments.
*   **AJAX Dynamic Search**: Real-time filtering for both Users and Departments as you type.
*   **AJAX Smart Delete**: Perform deletions with an asynchronous confirmation and a smooth row fade-out animation.
*   **Strict Model Validation**: Data Integrity ensured via Data Annotations (`[Required]`, `[EmailAddress]`).
*   **Remote Validation**: Real-time uniqueness checks for Emails and Department Names using AJAX.
*   **Relational Database**: Demonstrates a One-to-Many relationship (One Department -> Multiple Users).
*   **Entity Framework ORM**: Uses Code-First migrations to automatically sync database tables with C# objects.

## 🛠️ Technology Stack

*   **Framework**: .NET Framework 4.7.2
*   **Architecture**: MVC (Model-View-Controller)
*   **ORM**: Entity Framework 6 (Code-First)
*   **Database**: SQL Server (LocalDB / Express)
*   **UI**: HTML, CSS (Bootstrap), Razor View Engine (`.cshtml`)
*   **Libraries**: jQuery (used for AJAX and UI animations)

## 🗄️ Database Structure

**`Departments` Table**
* `DepartmentId` (Primary Key)
* `DepartmentName` (Name of the department)

**`Users` Table**
* `UserId` (Primary Key)
* `UserName` (Name of the user)
* `Email` (Validated unique email)
* `DepartmentId` (Foreign Key referencing `Departments`)

## 🚀 Getting Started

### Prerequisites
* Visual Studio 2022
* SQL Server Express / LocalDB

### Installation & Setup

1. **Clone & Open**
   Open `OrganizationMVC.sln` using Visual Studio.

2. **Restore Packages**
   Visual Studio should automatically restore NuGet packages. If not, run `Update-Package -Reinstall` in the Package Manager Console.

3. **Database Setup (Migrations)**
   Run these commands in the **Package Manager Console** to build your database:
   ```powershell
   Enable-Migrations
   Add-Migration InitialCreate
   Update-Database
   ```

4. **Update for Validation**
   If you have made changes to the models, run:
   ```powershell
   Add-Migration AddedValidation
   Update-Database
   ```

5. **Run**
   Press **F5** to build and launch the project in your browser.

## 📸 Screenshots
*(Add screenshots of your User Index and Department Search pages here!)*

## 📝 License
This project is for educational and assignment purposes.
