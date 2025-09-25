# Setup Instructions for Website Blog API

## Prerequisites

You need to install .NET 8.0 SDK to run this API.

### Download and Install .NET 8.0 SDK

1. Go to: https://dotnet.microsoft.com/download/dotnet/8.0
2. Download the .NET 8.0 SDK for Windows
3. Run the installer and follow the installation steps
4. Restart your command prompt/PowerShell after installation

### Verify Installation

After installing, open a new PowerShell window and run:
```bash
dotnet --version
```

You should see version 8.0.x displayed.

## Running the API

Once .NET is installed:

1. Open PowerShell and navigate to the project directory:
   ```bash
   cd "C:\Code\Website-API"
   ```

2. Restore the NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the API:
   ```bash
   dotnet run
   ```

5. The API will start and display URLs like:
   ```
   Now listening on: http://localhost:5000
   Now listening on: https://localhost:5001
   ```

6. Open your browser and go to `https://localhost:5001` to see the Swagger documentation

## Testing the API

### Using Swagger UI
- Navigate to `https://localhost:5001` in your browser
- You'll see the interactive Swagger documentation
- You can test all endpoints directly from the browser

### Using curl or Postman
```bash
# Get all published blog posts
curl https://localhost:5001/api/blogposts?published=true

# Search for posts
curl "https://localhost:5001/api/blogposts/search?query=dotnet"

# Get a specific post
curl https://localhost:5001/api/blogposts/1
```

## Connecting to Your Frontend

Your frontend can make requests to:
- Base URL: `https://localhost:5001`
- API endpoints: `/api/blogposts/*`

Example JavaScript:
```javascript
// Get all published posts
const response = await fetch('https://localhost:5001/api/blogposts?published=true');
const posts = await response.json();

// Create a new post
const newPost = await fetch('https://localhost:5001/api/blogposts', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        title: "My Post",
        content: "Post content here",
        author: "Your Name",
        isPublished: true
    })
});
```

## Sample Data

The API comes pre-loaded with 3 sample blog posts for testing. You can view them by calling the GET endpoints or through Swagger UI.

## Troubleshooting

1. **Port already in use**: If ports 5000/5001 are busy, the API will automatically use different ports
2. **HTTPS certificate issues**: For development, you may need to trust the development certificate:
   ```bash
   dotnet dev-certs https --trust
   ```
3. **CORS issues**: The API is configured to allow all origins for development. Update CORS settings in Program.cs for production use.

## Next Steps

Once the API is running successfully:
1. Test the endpoints using Swagger UI
2. Integrate with your frontend blog website
3. Customize the blog post model if needed
4. Consider switching to a persistent database for production use
