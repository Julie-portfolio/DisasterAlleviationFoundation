
# 📘 Disaster Alleviation Foundation

A web application built to streamline disaster relief efforts by managing donations, fund allocations, and disaster response activities. Developed as part of a final‑year Software Development project, the system demonstrates **front‑end, back‑end, and cloud deployment** skills with automated testing and CI/CD pipelines.

---

## 🚀 Features
- **Donations Management**: Capture and track donations (money, food, clothing, etc.).  
- **Disaster Tracking**: Record and monitor disaster events.  
- **Fund Allocation**: Assign funds to specific disasters.  
- **User Authentication**: Secure login and role‑based access.  
- **Responsive UI**: Optimized for desktop and mobile.  

---

## 🛠️ Tech Stack
- **Frontend**: ASP.NET Core MVC, Razor Views, Bootstrap  
- **Backend**: C# .NET 8, Entity Framework Core  
- **Database**: Azure SQL Database  
- **Testing**: MSTest (unit, integration), Selenium (UI, headless Chrome), JMeter (load & stress)  
- **CI/CD**: Azure DevOps Pipelines (build, test, deploy)  
- **Hosting**: Azure App Service  

---

## 📂 Project Structure
```
DisasterAlleviationFoundation.sln
 ├── DisasterAlleviationFoundation/     # Main web app
 ├── DisasterAlleviationFoundation.Tests/ # Unit & integration tests
 ├── DisasterAlleviationFoundation.UITests/ # Selenium UI tests
 └── azure-pipelines.yml                # CI/CD pipeline definition
```

---

## 🧪 Testing
- **Unit Tests**: Run with `dotnet test` or via Visual Studio Test Explorer.  
- **Integration Tests**: Validate DB and API endpoints.  
- **UI Tests**: Selenium WebDriver with headless Chrome.  
- **Load/Stress Tests**: Apache JMeter `.jmx` plans simulating 50, 100, and 500 concurrent users.  

---

## ⚙️ CI/CD Pipeline
- Triggered on every push to `main`.  
- Steps: Restore → Build → Test → Deploy.  
- Deploys automatically to **Azure App Service**.  
- Includes rollback support via deployment slots.  

---

## 📊 Reports
- **Unit & Integration**: Code coverage and pass/fail rates.  
- **UI**: Automated browser tests in headless mode.  
- **Load/Stress**: JMeter reports (response times, throughput, error %).  

---

## 👥 Usability Feedback
- Users found the donation form intuitive.  
- Suggested clearer error messages and confirmation alerts.  
- Planned improvements: friendlier validation messages, success notifications, and mobile layout refinements.  

---

## 📦 Getting Started

### Prerequisites
- .NET 8 SDK  
- SQL Server / Azure SQL  
- Node.js (optional, for front‑end tooling)  
- Apache JMeter (for load testing)  

### Run Locally
```bash
git clone https://github.com/Julie-portfolio/DisasterAlleviationFoundation.git
cd DisasterAlleviationFoundation
dotnet restore
dotnet build
dotnet run
```
App will be available at `https://localhost:5001`.

---

## 📜 License
This project is for academic purposes. All rights reserved © 2025.

---


---

Do you want me to also **add badges** (like build status, test coverage, deployment) at the top of the README so it looks even more polished on GitHub?
