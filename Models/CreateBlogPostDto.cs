using System.ComponentModel.DataAnnotations;

namespace WebsiteAPI.Models
{
    public class CreateBlogPostDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Summary { get; set; }
        
        [MaxLength(100)]
        public string Author { get; set; } = "Anonymous";
        
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        
        public List<string> Tags { get; set; } = new List<string>();
        
        public bool IsPublished { get; set; } = false;
    }
}
