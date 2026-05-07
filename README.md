# Library Management System

A robust, desktop-based Library Management System built using C# Windows Forms (.NET Framework) and SQL Server. This application provides a comprehensive suite of tools for librarians to manage books, members, staff, publishers, and daily circulation tasks (issuing and returning books).

## Features

* **Dashboard:** A central hub to navigate through different management modules. Checks SQL Server connection status on startup.
* **Main Desk (Circulation):** * Check-out (Issue) books to members.
    * Process book returns.
    * View real-time available inventory and active loans.
    * Visual alerts for overdue books.
* **Book Management:**
    * Add new books with details like ISBN, Title, Category, Author, Pages, Staff ID, and Publisher ID.
    * Update existing book details.
    * Delete books (with validation to prevent deleting borrowed books).
* **Stock Manager:**
    * Manage specific copies of a book.
    * Add or remove individual copies from the master inventory.
* **Member Directory:**
    * Register new members with contact details.
    * Update member information.
    * Remove members (prevents deletion if they have active loans).
* **Staff Management:**
    * Add, update, and remove staff members.
    * Search staff by ID.
* **Publisher Management:**
    * Manage publisher records including contact details and addresses.

## Prerequisites

* [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/net472)
* [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)
* Visual Studio 2019/2022 (recommended for development)

## Setup & Installation


1.  **Database Setup:**
    * Ensure your SQL Server is running locally (`Data Source=.`).
    * Create a database named `LibrarySystem`.
    * Run the necessary SQL scripts to create the required tables: `Books`, `BookCopy`, `Member`, `Staff`, `Publisher`, and `PublisherAddress`.
2.  **Configuration:**
    * The application connects to the local SQL Server instance by default. The connection string is defined in `Program.cs`:
        ```csharp
        public static string ConnectionString = "Data Source=.;Initial Catalog=LibrarySystem;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        ```
    * Modify this string if your SQL Server setup requires a different configuration (e.g., specific user/password or named instance).
3.  **Run the Application:**
    * Open `LibrarySystem.sln` in Visual Studio.
    * Build and run the project (F5).

## Tech Stack

* **Language:** C#
* **UI Framework:** Windows Forms (WinForms)
* **Database:** Microsoft SQL Server
* **Data Access:** ADO.NET (`System.Data.SqlClient`)