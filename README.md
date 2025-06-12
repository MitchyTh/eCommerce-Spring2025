# Full-Stack .NET C# E-Commerce Site Prototype

## Overview
This is a full-stack e-commerce site prototype built with .NET and C#. The application demonstrates key features of an e-commerce platform including file-based data persistence, search-driven queries, inventory management, shopping cart functionality, and a backend server connected to a frontend client. Although not yet operational as a live product, this prototype serves as a foundation for further development and enhancement.

## Features
- **File-based Persistence:** Stores product, user, and order data in local files for simplicity and easy setup  
- **Search Queries:** Enables searching products based on keywords and filters  
- **Inventory Management:** Tracks product stock levels and availability  
- **Shopping Cart:** Allows users to add, update, and remove items from their cart  
- **Backend-Frontend Communication:** RESTful API built with .NET Web API connected to a frontend interface  
- **Modular Architecture:** Separation of concerns for easy maintenance and extensibility  

## Technologies Used
- **Backend:** ASP.NET Core Web API, C#  
- **Frontend:** Built using .NET MAUI, implementing an MVVM pattern. UI designed with XAML, and behavior controlled with C#  
- **Data Storage:** File-based persistence using locally stored JSON files, with serialization and deserialization handled by Newtonsoft.Json for reliable saving and loading of inventory and cart data  
- **Version Control:** Git  

## Getting Started
1. Clone the repository  
2. Open the solution in Visual Studio 2022 or later  
3. Restore NuGet packages  
4. Build the solution  
5. Run the backend server project to start the API  
6. Open the frontend project (if separate) and run it to interact with the backend  

## Future Work
- Integrate database support (e.g., SQL Server, PostgreSQL)  
- Add user authentication and authorization  
- Implement secure payment gateway integration  
- Improve frontend UI/UX for better user experience  
- Add unit and integration tests  

## Contributing
This project is currently a personal prototype. Contributions are welcome as suggestions or forks for experimentation.

