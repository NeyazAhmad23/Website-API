using System.ComponentModel.DataAnnotations;

namespace WebsiteAPI.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Summary { get; set; }
        
        [MaxLength(100)]
        public string Author { get; set; } = "Anonymous";
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsPublished { get; set; } = false;
        
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        
        public List<string> Tags { get; set; } = new List<string>();
        
        public int ViewCount { get; set; } = 0;
    }
}
