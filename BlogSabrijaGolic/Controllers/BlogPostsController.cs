using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogSabrijaGolic.Models;

namespace BlogSabrijaGolic.Controllers
{
    [Produces("application/json")]
    [Route("api/posts")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogPostContext _context;

        public BlogPostsController(BlogPostContext context)
        {
            _context = context;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public IActionResult GetBlogPosts()
        {

            return Ok(new { BlogPosts = _context.BlogPost.Include(x => x.TagList).Select(x => new BlogPostModel() { ID=x.ID, Slug = x.Slug, Title = x.Title, Description = x.Description,
                Body = x.Body, TagList = ReturnTags(x.TagList), CratedAt = x.CratedAt, UpdatedAt = x.UpdatedAt
            }), PostsCount = _context.BlogPost.Count() });
        }
        // GET: api/BlogPosts?tag=android
        /*[HttpGet]
        public IActionResult GetBlogPosts([FromQuery]string tag)
        {
            BlogPostTag blogPostTag = new BlogPostTag();
            blogPostTag.Tag.Name = tag;
           
            return Ok(new { BlogPosts = _context.BlogPost.Where(x => x.TagList.Contains(blogPostTag)), PostsCount = _context.BlogPost.Count() });
        }*/

        // GET: api/BlogPosts/generic-slug
        [HttpGet("{slug}")]
        public async Task<IActionResult> GetBlogPost([FromRoute] string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogPost = await _context.BlogPost.SingleOrDefaultAsync(x => x.Slug.Equals(slug));

            if (blogPost == null)
            {
                return NotFound();
            }

            return Ok(blogPost);
        }

        // PUT: api/BlogPosts/generic-slug
        [HttpPut("{slug}")]
        public async Task<IActionResult> PutBlogPost([FromRoute] string slug, [FromBody] BlogPost blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (slug != blogPost.Slug)
            {
                return BadRequest();
            }
            
            _context.Entry(blogPost).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(slug))
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

        // POST: api/BlogPosts
        [HttpPost]
        public async Task<IActionResult> PostBlogPost([FromBody] BlogPostModel blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dateTime = DateTime.Now;
            
            BlogPost blog = new BlogPost
            {
                Slug = SlugCreator.GetFriendlyTitle(blogPost.Title, true),
                Title = blogPost.Title,
                Description = blogPost.Description,
                Body = blogPost.Body,
                CratedAt = dateTime,
                UpdatedAt = dateTime

            };
            foreach (string tag in blogPost.TagList)
            {
                _context.BlogPostTags.Add(new BlogPostTag { Tag = new Tag { Name = tag }, BlogPost = blog });
                //_context.Tag.Add(new Tag{ Name = tag });                
            }          
            
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBlogPosts", new { id = blogPost.ID }, blogPost);
        }

        // DELETE: api/BlogPosts/generic-slug
        [HttpDelete("{slug}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogPost = await _context.BlogPost.FirstOrDefaultAsync(x => x.Slug.Equals(slug));
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();

            return Ok(blogPost);
        }

        private bool BlogPostExists(string slug)
        {
            return _context.BlogPost.Any(e => e.Slug == slug);
        }
        private List<string> ReturnTags(List<BlogPostTag> blogPostTag)
        {
            List<string> ListOfTag = new List<string>();
            foreach (BlogPostTag tag in blogPostTag)
            {
                ListOfTag.Add(tag.Tag.Name);
            }
            return ListOfTag;
        }
    }

    
}