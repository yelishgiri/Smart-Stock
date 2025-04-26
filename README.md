# Smart Stock

## Project Background
Smart Stock is a user-friendly, real-time inventory management application designed for small businesses, especially those in regions where inventory is still tracked manually using outdated methods like paper logs or spreadsheets. These traditional methods are inefficient, prone to human error, and lack real-time data accessibility.

Smart Stock aims to solve these problems by offering:
- Secure authentication
- CRUD (Create, Read, Update, Delete) operations
- Barcode Insertion
- And more

Developed using .NET MAUI, C# and SQLite Database Smart Stock is both technically robust and accessible to non-technical users, empowering small business owners to make better, data-driven decisions while minimizing administrative overhead.

## Features and Prototype

### Authentication
- **Register Page**: New users can create an account by entering their username, company name, email, and password
- **Login Page**: Existing users can securely access their account
- **Password Recovery**: Includes forgot password functionality with email verification

### Dashboard
The main hub providing:
- Visual overview of analytics
- Current stock data
- Recently added items
- Navigation to other features

### Product Management
- Search, filter, and manage product inventory
- Add new products using barcode input
- Categorize products
- Track quantity
- View latest and low stock products

## Navigation Flow
The application follows a logical flow:
1. Authentication (Register/Login) 
2. Dashboard as central hub
3. Access to Products, Dashboard
4. Secure logout from any major screen


# SmartStock Inventory Management Documentation

## Overview

SmartStock is a cross-platform inventory management application built with .NET MAUI. It provides real-time tracking of inventory items, supports barcode scanning, and offers intuitive dashboards. Data is stored locally using SQLite, and the app follows a modular MVVM-inspired architecture.

**Key Features:**

- User authentication (Login, Registration, Password Reset)
- Full product CRUD operations with barcode support
- Inventory analytics dashboard
- Low stock alerts
- Sales invoice generation

## Requirements

**Programming Language:**

- C# using the .NET MAUI framework

**External Libraries:**

- SQLite-net-pcl v1.8.116
- CommunityToolkit.Maui v5.2.0
- Material Design Icons

**Supported Platforms:**

- Windows 10+

## System Design

### Main Components

#### 1. Login Page

- User credential validation
- Navigation to registration and password reset
- Session initialization

**Files:**
- `LoginPage.xaml`: UI layout
- `LoginPage.xaml.cs`: Authentication logic

#### 2. Products Page

- Add/Edit products
- Real-time inventory search
- Low stock visualization

**Files:**
- `ProductsPage.xaml`: Product form layout
- `ProductsPage.xaml.cs`: Product operations

#### 3. Dashboard Page

- Displays inventory KPIs
- Shows recent transactions
- Quick access to features

**Files:**
- `DashboardPage.xaml`: Layout
- `DashboardPage.xaml.cs`: Data aggregation

## File Breakdown

| File             | Purpose                               |
|------------------|----------------------------------------|
| `Product.cs`     | Defines product schema                 |
| `Users.cs`       | Stores user credentials and info       |
| `AppShell.xaml`  | Main navigation structure              |

## Design Notes

**Security:**

- Current: Plaintext password storage
- Recommended: Implement bcrypt hashing

**Navigation:**

- Uses .NET MAUI Shell URI routing:
  ```csharp
  Shell.Current.GoToAsync(nameof(ProductsPage));
  ```

**Data Binding:**

- `CollectionView` binds to `ObservableCollection<Product>`

## Testing Strategy

### Unit Tests

**Product Class:**
```csharp
[Test]
public void Product_TotalValue_CalculatesCorrectly() {
  var p = new Product { Price = 10, Quantity = 5 };
  Assert.AreEqual(50, p.Price * p.Quantity);
}
```

**LoginPage Validation:**

- Test rejection of empty username/password

### Integration Tests

- Login -> Dashboard
- Product addition -> Dashboard update

### Boundary Tests

- Product Quantity: negative/10,000+ values
- Price: Max decimal precision

## Test Cases

| ID    | Scenario                          | Expected Result                     |
|-------|-----------------------------------|--------------------------------------|
| TC01  | Login with invalid credentials    | Show "Invalid credentials" alert     |
| TC02  | Add product with empty name       | Block submission, show error         |

## Sample Test Scenarios

**Scenario 1: Low Stock Alert**

1. Set product quantity to 8 (threshold = 10)
2. Navigate to Products Page
3. Verify item appears in red in Low Stock section

**Scenario 2: Duplicate User Registration**

1. Register with username: testuser
2. Attempt to register again
3. Expected: Show "Username already exists"


## Authors
- Rabindra Giri 
- Saleep Shrestha
- Saqib Mahmood
- Himanshu Shah
- Arinze Chiekwu
