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
        
        // GET: api/BlogPosts?tag=android
        [HttpGet]
        public IActionResult GetBlogPosts([FromQuery]string tag)
        {
            var blogPostsAll = _context.BlogPost.Select(bp => new BlogPostModel()
            {
                ID = bp.ID,
                Slug = bp.Slug,
                Title = bp.Title,
                Description = bp.Description,
                Body = bp.Body,
                TagList = bp.TagList.Select(tl => tl.Tag.Name).ToList(),
                CratedAt = bp.CratedAt,
                UpdatedAt = bp.UpdatedAt
            });
            if (tag != null)
            {
                var blogPostsFiltered = blogPostsAll.Where(x => x.TagList.Contains(tag)).OrderByDescending( x=> x.CratedAt).ToList();
                return Ok(new { BlogPosts = blogPostsFiltered, PostsCount = blogPostsFiltered.Count() });                
            }
            else
            {
                return Ok(new { BlogPosts = blogPostsAll, PostsCount = blogPostsAll.Count() });

            }
        }
        

        // GET: api/posts/generic-slug
        [HttpGet("{slug}")]
        public IActionResult GetBlogPost([FromRoute] string slug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogPost = _context.BlogPost.Select(bp => new BlogPostModel()
            {
                ID = bp.ID,
                Slug = bp.Slug,
                Title = bp.Title,
                Description = bp.Description,
                Body = bp.Body,
                TagList = bp.TagList.Select(tl => tl.Tag.Name).ToList(),
                CratedAt = bp.CratedAt,
                UpdatedAt = bp.UpdatedAt
            }).Where(x => x.Slug.Equals(slug));
            if (blogPost == null)
            {
                return NotFound();
            }
            return Ok(blogPost);
        }

        // PUT: api/posts/generic-slug
        [HttpPut("{slug}")]
        public async Task<IActionResult> PutBlogPostAsync([FromRoute] string slug, [FromBody] BlogPostModel blogPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!BlogPostExists(slug))
            {
                return NotFound();
            }

            BlogPost blog = _context.BlogPost.FirstOrDefault(x => x.Slug.Equals(slug));

            if(blogPost.Title != null)
            {
                blog.Title =  blogPost.Title;
                
            }
            if (blogPost.Description != null)
            {
                blog.Description = blogPost.Description;

            }
            if (blogPost.Body != null)
            {
                blog.Body = blogPost.Body;

            }
            blog.UpdatedAt = DateTime.Now;

            _context.Entry(blog).State = EntityState.Modified;
           
            
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
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

        // POST: api/posts
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
                Slug = SlugCreator.GetFriendlyTitle(blogPost.Title + "-" + dateTime.ToUniversalTime().ToString(), true),
                Title = blogPost.Title,
                Description = blogPost.Description,
                Body = blogPost.Body,
                CratedAt = dateTime,
                UpdatedAt = dateTime
            };            
            foreach (string tag in blogPost.TagList)                
            {
                if (_context.Tag.FirstOrDefault(x => x.Name.Equals(tag)) == null)
                {
                    _context.BlogPostTags.Add(new BlogPostTag { Tag = new Tag { Name = tag }, BlogPost = blog });
                }
                else
                {
                    _context.BlogPostTags.Add(new BlogPostTag { Tag = _context.Tag.FirstOrDefault(x => x.Name.Equals(tag)), BlogPost = blog });
                }
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBlogPosts", new { id = blogPost.ID }, blogPost);
            
        }

        // DELETE: api/posts/generic-slug
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

    }


}