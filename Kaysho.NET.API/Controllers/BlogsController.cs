using DamilolaShopeyin.Core.Models;
using DamilolaShopeyin.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DamilolaShopeyin.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IRepository<Blog> _repository;
        public BlogsController(IRepository<Blog> blogRepository)
        {
            this._repository = blogRepository;
        }

        // GET: api/Blogs
        [HttpGet]
        public ActionResult GetBlogs()
        {
            return Ok(_repository.GetAll());
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public ActionResult GetBlog(int id)
        {
            var blog = _repository.Get(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // PUT: api/Blogs/5
        [HttpPut("{id}")]
        public IActionResult PutBlog(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            var oldBlog = _repository.Get(blog.Id);
            if (oldBlog == null) return NotFound($"Could not find blog with id of {blog.Id}");

            try
            {
                _repository.Update(blog);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<IActionResult> PostBlog(Blog blog)
        {

            _repository.Add(blog);

            var result = await _repository.Commit();

            if (result)
            {
                return CreatedAtAction(nameof(GetBlog), new { id = blog.Id }, blog);
            }

            return BadRequest();
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public ActionResult<Blog> DeleteBlog(int id)
        {
            var blog = _repository.Get(id);
            if (blog == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return blog;
        }


    }
}
