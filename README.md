# Markdown Note Taking API

A RESTful API for creating, managing, and rendering Markdown notes with user authentication.

## Overview

This project provides a backend service for a markdown-based note taking application. Users can create, read, update, and delete markdown notes. The API also provides functionality to render markdown notes as HTML 

## Features

- **User Authentication**: Secure JWT-based authentication system
- **Markdown Note Management**: Create, read, update, and delete markdown notes
- **HTML Rendering**: Convert markdown notes to HTML for rendering
- **Access Control**: Notes are private and can only be accessed by their creators

## Technologies Used

- ASP.NET Core 9.0
- Entity Framework Core 8.0
- MySQL Database (via Pomelo.EntityFrameworkCore.MySql)
- JWT Authentication
- Markdig for Markdown processing
- AutoMapper for object mapping
- Swagger/OpenAPI for API documentation

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- MySQL Server
- Visual Studio, Visual Studio Code, or another IDE with .NET support

### Installation

1. Clone the repository
   ```
   git clone https://github.com/yourusername/Markdown-Note-taking-Api.git
   cd Markdown-Note-taking-Api
   ```

2. Configure the database connection in `appsettings.json`
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=markdownnotes;User=root;Password=yourpassword;"
   }
   ```

3. Set up JWT authentication settings in `appsettings.json`
   ```json
   "JWT": {
     "Issuer": "MarkdownNoteApi",
     "Audience": "MarkdownNoteApiClients",
     "SigningKey": "your-secret-key-here-make-it-long-and-secure"
   }
   ```

4. Run database migrations
   ```
   dotnet ef database update
   ```

5. Build and run the application
   ```
   dotnet build
   dotnet run
   ```

6. Access Swagger UI at `https://localhost:5001/swagger`

## API Endpoints

### Authentication

- **POST** `/api/Auth/register` - Register a new user
- **POST** `/api/Auth/login` - Login and receive JWT token

### Notes

- **POST** `/api/Note` - Create a new note
- **GET** `/api/Note/{noteId}` - Get a specific note in markdown format
- **GET** `/api/Note/{noteId}/html` - Get a specific note rendered as HTML
- **PUT** `/api/Note/{noteId}` - Update a note
- **DELETE** `/api/Note/{noteId}` - Delete a note


## Project Structure

```
MarkDownNoteApi/
├── Controller/             # API controllers
├── Data/                   # Database context and configurations
├── Dtos/                   # Data transfer objects
├── Extensions/             # Extension methods
├── Interface/              # Service and repository interfaces
├── Migrations/             # EF Core migrations
├── Models/                 # Domain models
├── Repositories/           # Data access implementations
├── Service/                # Business logic implementations
├── uploads/                # Markdown file storage (created at runtime)
├── appsettings.json        # Application settings
├── Program.cs              # Application entry point and setup
└── MarkDownNoteApi.csproj  # Project file
```

## Authentication Flow

1. User registers with email and password
2. User logs in and receives JWT token
3. Subsequent API requests include the JWT token in the Authorization header
4. API validates token and identifies user for operations

## File Storage

Markdown files are stored on the server's filesystem in an 'uploads' directory. Each file is given a unique name based on a GUID to prevent conflicts.

## License

[MIT License](LICENSE)

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

For bugs, feature requests, or additional information, please open an issue in the GitHub repository.

