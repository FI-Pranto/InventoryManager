# 📦 InventoryManager

A modern and responsive **Inventory Management System** built with **ASP.NET Core MVC**, designed to help small and medium businesses manage their products, track stock levels, and streamline operations through a user-friendly interface and real-time data.

---

## 🚀 Features

- 🧾 **Product Management** — Create, edit, view, and delete products
- 🔍 **Search & Sorting** — Filter products by name and sort by price or name
- 📸 **Image Upload** — Upload and display product images
- 📈 **Pagination** — User-friendly paging with dynamic page navigation
- 📊 **Dashboard Animation** — Visually appealing 3D animation using CSS
- ✅ **Responsive UI** — Mobile-friendly design using Bootstrap 5
- 🧩 **Clean Architecture** — Follows MVC pattern with separation of concerns

---

## 🛠 Technologies Used

- **ASP.NET Core MVC 7**
- **Entity Framework Core**
- **SonarCloud for code Quality**
- **Github Action for CI/CD**
- **Bootstrap 5**
- **SQL Server (LocalDB)**
- **Font Awesome Icons**
- **Custom CSS Animations**
- **Razor Views**

---

## 📸 Screenshots

> _Add screenshots here if you have them (e.g. homepage, product list, etc.)_

---

## 🧰 Setup Instructions

### 1. Clone the repository

```bash
git clone https://github.com/FI-Pranto/InventoryManager.git
cd InventoryManager
```


## 🛠️ Getting Started

### 2️⃣ Setup the Database

1. Open the `appsettings.json` file and update the connection string if needed:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InventoryManagerDb;Trusted_Connection=True;"
}
```

2. Apply the EF Core migrations to create the database:

```bash
dotnet ef database update
```

---

### 3️⃣ Run the Application

Start the application using the .NET CLI:

```bash
dotnet run
```

Then open your browser and navigate to:

```
http://localhost:5000
```

> ℹ️ Alternatively, if using Visual Studio, press **F5** to build and run the project.



