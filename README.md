# 🔗 URL Shortener API

A **Minimal API** built with **ASP.NET Core 8** for shortening URLs.  
It generates **unique short codes** for long URLs and allows redirecting users to the original URLs.  
Designed for lightweight and fast deployment, following **clean architecture principles**.

---

## 🚀 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)  
- **ASP.NET Core Minimal API**  
- **Entity Framework Core**  
- **SQLite** (lightweight database for URL storage)  
- **Swagger / OpenAPI** (automatic API documentation)  

---

## 📂 Project Structure

/Entities
├── ShortenerUrl.cs # URL entity model
/Models
├── ShortenerUrlRequest.cs # Request DTO for shortening
/Services
├── UrlShorteningService.cs # Service for generating unique codes
/Infrastructure
├── ApplicationDbContext.cs # EF Core DbContext
/Extensions
├── MigrationExtensions.cs # Applies migrations on startup
Program.cs # Minimal API configuration

yaml
Copiar código

---

## 📌 Features

- Shorten any valid URL with a **unique code**  
- Redirect short URLs to the original long URL  
- Validate URL format before shortening  
- Minimal API structure for lightweight and fast deployment  
- Automatic API documentation via Swagger  

---

## 🔗 API Endpoints

### 1. Shorten URL
- **POST** `/api/shortener`  
- **Request Body**
```json
{
  "url": "https://example.com/long-url"
}
Response

json
Copiar código
"https://localhost:5001/api/AbCd12"
cURL Example

bash
Copiar código
curl -X POST "https://localhost:5001/api/shortener" \
     -H "Content-Type: application/json" \
     -d '{"url":"https://example.com/long-url"}'
2. Redirect Short URL
GET /api/{code}

Redirects to the original URL.

Responses

302 Found → redirects to long URL

404 Not Found → if code does not exist

cURL Example

bash
Copiar código
curl -i "https://localhost:5001/api/AbCd12"
⚙️ Getting Started
Prerequisites
.NET 8 SDK

Run the project
bash
Copiar código
# Clone repository
git clone https://github.com/your-username/url-shortener-api.git

# Navigate into project folder
cd url-shortener-api

# Restore dependencies
dotnet restore

# Run the project
dotnet run
The API will be available at:

Swagger UI: https://localhost:5001/swagger

Base URL: https://localhost:5001/api

🛠 Configuration
The project uses SQLite by default. Connection string is defined in appsettings.json:

json
Copiar código
"ConnectionStrings": {
  "DefaultConnection": "Data Source=urls.db"
}
On startup, the database migrations are automatically applied.

📌 Future Improvements
Allow custom short codes

Add expiration dates for short URLs

Implement user authentication & dashboards

Provide analytics for URL clicks

Add Docker support and CI/CD pipelines
