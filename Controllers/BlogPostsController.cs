using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteAPI.Data;
using WebsiteAPI.Models;

namespace WebsiteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogContext _context;

        public BlogPostsController(BlogContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetBlogPosts(
            [FromQuery] bool? published = null,
            [FromQuery] string? author = null,
            [FromQuery] string? tag = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.BlogPosts.AsQueryable();

            // Filter by published status
            if (published.HasValue)
            {
                query = query.Where(bp => bp.IsPublished == published.Value);
            }

            // Filter by author
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(bp => bp.Author.Contains(author));
            }

            // Filter by tag
            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(bp => bp.Tags.Contains(tag));
            }

            // Order by creation date (newest first)
            query = query.OrderByDescending(bp => bp.CreatedAt);

            // Apply pagination
            var totalCount = await query.CountAsync();
            var blogPosts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Page", page.ToString());
            Response.Headers.Add("X-Page-Size", pageSize.ToString());

            return Ok(blogPosts);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            // Increment view count
            blogPost.ViewCount++;
            await _context.SaveChangesAsync();

            return blogPost;
        }

        // GET: api/BlogPosts/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> SearchBlogPosts(
            [FromQuery] string query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Search query cannot be empty");
            }

            var searchQuery = _context.BlogPosts
                .Where(bp => bp.IsPublished &&
                    (bp.Title.Contains(query) ||
                     bp.Content.Contains(query) ||
                     bp.Summary!.Contains(query) ||
                     bp.Tags.Any(tag => tag.Contains(query))))
                .OrderByDescending(bp => bp.CreatedAt);

            var totalCount = await searchQuery.CountAsync();
            var blogPosts = await searchQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Response.Headers.Add("X-Total-Count", totalCount.ToString());
            Response.Headers.Add("X-Page", page.ToString());
            Response.Headers.Add("X-Page-Size", pageSize.ToString());

            return Ok(blogPosts);
        }

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreateBlogPost(CreateBlogPostDto createDto)
        {
            var blogPost = new BlogPost
            {
                Title = createDto.Title,
                Content = createDto.Content,
                Summary = createDto.Summary,
                Author = createDto.Author,
                ImageUrl = createDto.ImageUrl,
                Tags = createDto.Tags,
                IsPublished = createDto.IsPublished,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
        }

        // PUT: api/BlogPosts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogPost(int id, UpdateBlogPostDto updateDto)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(updateDto.Title))
                blogPost.Title = updateDto.Title;
            
            if (!string.IsNullOrEmpty(updateDto.Content))
                blogPost.Content = updateDto.Content;
            
            if (updateDto.Summary != null)
                blogPost.Summary = updateDto.Summary;
            
            if (!string.IsNullOrEmpty(updateDto.Author))
                blogPost.Author = updateDto.Author;
            
            if (updateDto.ImageUrl != null)
                blogPost.ImageUrl = updateDto.ImageUrl;
            
            if (updateDto.Tags != null)
                blogPost.Tags = updateDto.Tags;
            
            if (updateDto.IsPublished.HasValue)
                blogPost.IsPublished = updateDto.IsPublished.Value;

            blogPost.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/BlogPosts/tags
        [HttpGet("tags")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllTags()
        {
            var allTags = await _context.BlogPosts
                .Where(bp => bp.IsPublished)
                .SelectMany(bp => bp.Tags)
                .Distinct()
                .OrderBy(tag => tag)
                .ToListAsync();

            return Ok(allTags);
        }

        // GET: api/BlogPosts/authors
        [HttpGet("authors")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllAuthors()
        {
            var authors = await _context.BlogPosts
                .Where(bp => bp.IsPublished)
                .Select(bp => bp.Author)
                .Distinct()
                .OrderBy(author => author)
                .ToListAsync();

            return Ok(authors);
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
