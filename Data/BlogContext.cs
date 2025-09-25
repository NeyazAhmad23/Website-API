using Microsoft.EntityFrameworkCore;
using WebsiteAPI.Models;
using System.Text.Json;

namespace WebsiteAPI.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Tags property to be stored as JSON
            modelBuilder.Entity<BlogPost>()
                .Property(e => e.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>()
                );

            // Seed some sample data
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Welcome to My Blog",
                    Content = "This is my first blog post. I'm excited to share my thoughts and experiences with you!",
                    Summary = "A welcome message and introduction to the blog.",
                    Author = "Blog Admin",
                    CreatedAt = DateTime.UtcNow.AddDays(-7),
                    UpdatedAt = DateTime.UtcNow.AddDays(-7),
                    IsPublished = true,
                    ImageUrl = "https://via.placeholder.com/600x300?text=Welcome",
                    Tags = new List<string> { "welcome", "introduction" },
                    ViewCount = 15
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Getting Started with .NET Core",
                    Content = "In this post, we'll explore the basics of .NET Core development and how to build modern web applications.",
                    Summary = "A beginner's guide to .NET Core development.",
                    Author = "Tech Writer",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    UpdatedAt = DateTime.UtcNow.AddDays(-3),
                    IsPublished = true,
                    ImageUrl = "https://via.placeholder.com/600x300?text=.NET+Core",
                    Tags = new List<string> { "dotnet", "programming", "tutorial" },
                    ViewCount = 42
                },
                new BlogPost
                {
                    Id = 3,
                    Title = "Building RESTful APIs",
                    Content = "Learn how to design and implement RESTful APIs that are scalable, maintainable, and follow best practices.",
                    Summary = "Best practices for building RESTful APIs.",
                    Author = "API Expert",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedAt = DateTime.UtcNow.AddDays(-1),
                    IsPublished = false,
                    ImageUrl = "https://via.placeholder.com/600x300?text=REST+API",
                    Tags = new List<string> { "api", "rest", "backend" },
                    ViewCount = 8
                }
            );
        }
    }
}
