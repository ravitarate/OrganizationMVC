# Organization CRUD MVC Application

This is a complete ASP.NET Web MVC application designed to demonstrate Create, Read, Update, and Delete (CRUD) operations using Entity Framework Code-First and SQL Server LocalDB. 

## 📋 Features

* **Department Management**: Add, view, edit, and delete company departments.
* **User Management**: Add, view, edit, and delete users while assigning them to existing departments.
* **Relational Database**: Demonstrates a One-to-Many relationship (One Department -> Multiple Users).
* **Automated Scaffolding**: Utilizes built-in ASP.NET MVC scaffolding for quick view generation.
* **Entity Framework ORM**: Uses Code-First migrations to automatically generate and update database tables from C# objects.

## 🛠️ Technology Stack

* **Framework**: .NET Framework 4.7.2
* **Architecture**: MVC (Model-View-Controller)
* **ORM**: Entity Framework 6 (Code-First)
* **Database**: SQL Server (LocalDB by default)
* **UI**: HTML, Razor View Engine (`.cshtml`), Bootstrap CSS
* **IDE**: Visual Studio 2022

## 🗄️ Database Structure

The application maps to two primary database tables using Entity Framework:

**`Departments` Table**
* `DepartmentId` (INT, Primary Key)
* `DepartmentName` (NVARCHAR)

**`Users` Table**
* `UserId` (INT, Primary Key)
* `UserName` (NVARCHAR)
* `Email` (NVARCHAR)
* `DepartmentId` (INT, Foreign Key referencing `Departments`)

## 🚀 Getting Started

### Prerequisites
* Visual Studio 2019 or 2022
* SQL Server Express / LocalDB (included with Visual Studio)

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone <your-repository-url>
   ```

2. **Open the project**
   Open `OrganizationMVC.sln` using Visual Studio.

3. **Install NuGet Packages**
   Visual Studio should automatically restore missing packages. If not, open the **Package Manager Console** (`Tools > NuGet Package Manager > Package Manager Console`) and run:
   ```powershell
   Update-Package -Reinstall
   ```

4. **Run Entity Framework Migrations**
   To create the SQL Database exactly as defined by the C# Models, run these two commands in the **Package Manager Console**:
   ```powershell
   # If you haven't enabled migrations yet:
   Enable-Migrations

   # Generate the migration file
   Add-Migration InitialCreate

   # Apply the migration to SQL Server to create the actual tables
   Update-Database
   ```

5. **Run the Application**
   * Press **F5** in Visual Studio to build and launch the project.
   * To interact with Departments, navigate to `/Departments` in the browser.
   * To interact with Users, navigate to `/Users` in the browser.


## 📸 Screenshots
*(You can add screenshots of your User Index Page and Department Create Page here!)*

## 📝 License
This project is for educational and assignment purposes.
