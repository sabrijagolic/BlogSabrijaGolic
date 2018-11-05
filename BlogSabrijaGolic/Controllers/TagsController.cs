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
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly BlogPostContext _context;

        public TagsController(BlogPostContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public IActionResult GetTag()
        {

            return Ok(new { Tags = _context.Tag.Select(t => t.Name).ToList() });
        }

        
    }
}