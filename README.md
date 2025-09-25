# Website Blog API

A comprehensive .NET Core Web API for managing blog posts and content. This API is designed to work seamlessly with your frontend blog website.

## Features

- **Full CRUD operations** for blog posts
- **Search functionality** across titles, content, and tags
- **Filtering and pagination** support
- **Tag and author management**
- **View count tracking**
- **Published/Draft status** management
- **CORS enabled** for frontend integration
- **Swagger documentation** included
- **In-memory database** for easy development

## API Endpoints

### Blog Posts

- `GET /api/blogposts` - Get all blog posts (with filtering and pagination)
- `GET /api/blogposts/{id}` - Get a specific blog post
- `POST /api/blogposts` - Create a new blog post
- `PUT /api/blogposts/{id}` - Update a blog post
- `DELETE /api/blogposts/{id}` - Delete a blog post
- `GET /api/blogposts/search?query={searchTerm}` - Search blog posts
- `GET /api/blogposts/tags` - Get all available tags
- `GET /api/blogposts/authors` - Get all authors

### Query Parameters

#### GET /api/blogposts
- `published` (bool): Filter by published status
- `author` (string): Filter by author name
- `tag` (string): Filter by tag
- `page` (int): Page number (default: 1)
- `pageSize` (int): Items per page (default: 10)

#### GET /api/blogposts/search
- `query` (string): Search term
- `page` (int): Page number (default: 1)
- `pageSize` (int): Items per page (default: 10)

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code

### Running the API

1. Navigate to the project directory:
   ```bash
   cd Website-API
   ```

2. Restore packages:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. The API will be available at:
   - HTTP: `http://localhost:5000`
   - HTTPS: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001` (root path)

### Sample Data

The API comes with sample blog posts pre-loaded for testing:
- Welcome post
- .NET Core tutorial
- REST API best practices

## Database Configuration

By default, the API uses an in-memory database for easy development. To switch to SQL Server:

1. Uncomment the SQL Server configuration in `Program.cs`
2. Update the connection string in `appsettings.json`
3. Run Entity Framework migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

## Frontend Integration

This API is configured with CORS to allow requests from any origin. For production, update the CORS policy in `Program.cs` to restrict to your specific frontend domain.

### Example Frontend Usage

```javascript
// Get all published blog posts
const response = await fetch('https://localhost:5001/api/blogposts?published=true');
const blogPosts = await response.json();

// Search for posts
const searchResponse = await fetch('https://localhost:5001/api/blogposts/search?query=dotnet');
const searchResults = await searchResponse.json();

// Create a new post
const newPost = {
    title: "My New Post",
    content: "This is the content of my new post",
    summary: "A brief summary",
    author: "John Doe",
    tags: ["technology", "programming"],
    isPublished: true
};

const createResponse = await fetch('https://localhost:5001/api/blogposts', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(newPost)
});
```

## Health Check

The API includes a health check endpoint at `/health` that returns the current status and timestamp.

## Development Notes

- The API uses Entity Framework Core with Code First approach
- All blog post tags are stored as JSON arrays
- View counts are automatically incremented when posts are retrieved
- Pagination headers are included in responses (X-Total-Count, X-Page, X-Page-Size)
- All timestamps are stored in UTC

## Next Steps

- Add authentication and authorization
- Implement file upload for blog post images
- Add email notifications for new posts
- Implement caching for better performance
- Add logging and monitoring
