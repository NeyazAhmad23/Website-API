using System.ComponentModel.DataAnnotations;

namespace WebsiteAPI.Models
{
    public class UpdateBlogPostDto
    {
        [MaxLength(200)]
        public string? Title { get; set; }
        
        public string? Content { get; set; }
        
        [MaxLength(500)]
        public string? Summary { get; set; }
        
        [MaxLength(100)]
        public string? Author { get; set; }
        
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        
        public List<string>? Tags { get; set; }
        
        public bool? IsPublished { get; set; }
    }
}
