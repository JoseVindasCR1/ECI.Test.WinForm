# ECI.Test - Dog Walking Management System

A Windows Forms application built with .NET Framework 4.8 for managing dog walking services, featuring client management, dog profiles, walk tracking, and secure user authentication.

## Architecture

This application follows a clean architecture pattern with the following projects:

- **ECI.Test** - Windows Forms UI Layer
- **ECI.Test.BL** - Business Logic Layer (Services & Validators)
- **ECI.Test.DA** - Data Access Layer (Entity Framework & Repositories)
- **ECI.Test.Shared** - Shared Models, DTOs, and Utilities

##  Prerequisites

Before setting up the project, ensure you have:

- **Visual Studio 2019/2022** with .NET Framework 4.8 support
- **SQL Server** (LocalDB, Express, or Full) 
- **.NET Framework 4.8** installed
- **Entity Framework 6** (included via NuGet packages)

##  Database Configuration

> ** Important:** You need to update connection strings in TWO different App.config files depending on your needs.

### 1. Application Runtime Configuration

**For running the application, update:**
**File: `ECI.Test\App.config`** (Main application)

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <connectionStrings>
        <add name="DefaultConnection" 
             connectionString="Server=localhost;Database=ECITest;User Id=sa;Password=Qwerty123!;TrustServerCertificate=true;" 
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
    </startup>
</configuration>
```

### 2. Entity Framework Migration Configuration

**For running migrations manually, update:**
**File: `ECI.Test.DA\App.config`** (Data Access project)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
  </configSections>
  
  <connectionStrings>
    <add name="DefaultConnection" 
         connectionString="Server=localhost;Database=ECITest;User Id=sa;Password=Qwerty123!;TrustServerCertificate=true;" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
  
  <entityFramework>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
</configuration>
```

### 3. Connection String Options

Choose the appropriate connection string for your SQL Server setup and **update BOTH App.config files**:

#### **SQL Server with Authentication:**
```xml
<add name="DefaultConnection" 
     connectionString="Server=localhost;Database=ECITest;User Id=sa;Password=YourPassword;TrustServerCertificate=true;" 
     providerName="System.Data.SqlClient" />
```

#### **SQL Server with Windows Authentication:**
```xml
<add name="DefaultConnection" 
     connectionString="Server=localhost;Database=ECITest;Integrated Security=true;TrustServerCertificate=true;" 
     providerName="System.Data.SqlClient" />
```

#### **SQL Server LocalDB:**
```xml
<add name="DefaultConnection" 
     connectionString="Server=(localdb)\mssqllocaldb;Database=ECITest;Integrated Security=true;" 
     providerName="System.Data.SqlClient" />
```

### 4. Database Setup & Migration

The application uses Entity Framework Code First migrations. **You must run the migration manually** to create the database and tables.

#### **Required Steps:**

1. **Configure connection strings** in **BOTH** `App.config` files:
   - `ECI.Test\App.config` (for application runtime)
   - `ECI.Test.DA\App.config` (for Entity Framework migrations)

2. **Ensure SQL Server is running** and the database server is accessible

3. **Run the migration manually:**
   - Open **Package Manager Console** in Visual Studio (`Tools > NuGet Package Manager > Package Manager Console`)
   - Set **Default Project** to `ECI.Test.DA`
   - Run the following command:
   ```
   Update-Database
   ```

4. **Verify database creation:**
   - Connect to your SQL Server instance
   - Confirm the `ECITest` database has been created
   - Verify all tables exist: Users, Clients, Dogs, ClientDogs, Walks

5. **Run the application** to seed sample data

> **?? Note:** The Package Manager Console uses the connection string from `ECI.Test.DA\App.config`, while the running application uses `ECI.Test\App.config`.

## ??? Database Schema

The application creates the following tables:

| Table | Description |
|-------|-------------|
| **Users** | User accounts with secure password hashing |
| **Clients** | Dog owners and their contact information |
| **Dogs** | Dog profiles with breed and age information |
| **ClientDogs** | Many-to-many relationship between clients and dogs |
| **Walks** | Walk records with date, duration, and assignments |

## User Credentials

The application comes with pre-configured test users with secure password hashing:

| Username | Password | Description |
|----------|----------|-------------|
| **admin** | admin123 | Administrator account |
| **user1** | user123 | Standard user account |
| **walker1** | walker123 | Dog walker account |
| **manager** | manager123 | Manager account |

## Getting Started

### 1. Clone and Build
```bash
git clone [your-repository-url]
cd ECI.Test
# Open ECI.Test.sln in Visual Studio
# Build Solution (Ctrl+Shift+B)
```

### 2. Configure Database
- **Update connection string in `ECI.Test\App.config`** (for application runtime)
- **Update connection string in `ECI.Test.DA\App.config`** (for migrations)
- Ensure SQL Server is running and accessible
- **Both connection strings should point to the same database**

### 3. Create Database (Required)
**You must manually run the migration to create the database:**

1. Open **Package Manager Console** in Visual Studio
2. Set **Default Project** to `ECI.Test.DA`
3. Run: `Update-Database`
4. Verify the database and tables are created in SQL Server

### 4. Run Application
- Set `ECI.Test` as startup project
- Press F5 or click "Start"
- Sample data will be seeded on first run

### 5. Login
- Use any of the provided user credentials above
- Example: Username: `admin`, Password: `admin123`

## Application Features

### Main Features
- **User Authentication** - Secure login with password hashing
- **Client Management** - Add, edit, delete client profiles
- **Dog Management** - Manage dog profiles with breed and age
- **Walk Tracking** - Record and track dog walks
- **Data Validation** - Comprehensive input validation
- **Error Handling** - User-friendly error messages with logging

###  Technical Features
- **Dependency Injection** - Using Unity Container
- **Repository Pattern** - Clean data access layer
- **Custom Validation Framework** - FluentValidation-style validation
- **Secure Password Storage** - PBKDF2 hashing with salts
- **Entity Framework** - Code First approach with migrations
- **Logging** - Comprehensive error logging to files

##  Project Structure

```
ECI.Test/
??? ECI.Test/                  # Windows Forms UI
?   ??? Forms/                 # Windows Forms
?   ??? Utilities/             # UI utilities and logging
?   ??? App.config            # ? Runtime configuration
??? ECI.Test.BL/              # Business Logic
?   ??? Services/             # Business services
?   ??? Validators/           # Validation logic
??? ECI.Test.DA/              # Data Access
?   ??? Repositories/         # Repository implementations
?   ??? Migrations/           # EF migrations
?   ??? App.config            # ? Migration configuration
?   ??? DbContext.cs          # Entity Framework context
??? ECI.Test.Shared/          # Shared Components
    ??? Models/               # Entity models
    ??? DTOs/                 # Data transfer objects
    ??? Utilities/            # Shared utilities (password hashing)
```

##  Database Migration Details

### Migration Files
The application includes the following migration:
- `202601300443477_InitialCreate.cs` - Creates all tables (Users, Clients, Dogs, Walks, ClientDogs)

### Seed Data
Sample data is automatically created including:
- **5 test clients** with contact information
- **8 sample dogs** with different breeds and ages
- **Client-dog associations** 
- **Sample walk records**
- **4 test users** with hashed passwords

## Troubleshooting

### Logs
Application logs are written to:
```
%AppData%\ECI.Test\logs\error_yyyy-MM-dd.log
```
