using AutoMapper;
using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.Core.Models;
using DamilolaShopeyin.Core.Services;
using Kaysho.NET.Core.Contracts.V1;
using Kaysho.NET.Core.V1.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kaysho.NET.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class CommentsController : BaseController
    {
        private readonly IRepository<Comment> _repository;

        public CommentsController(IRepository<Comment> repository, UserManager<ApplicationUser> userMgr, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(userMgr, httpContextAccessor, mapper)
        {
            _repository = repository;
        }

        // GET: api/Comments
        [HttpGet(ApiRoutes.Comments.GetAll)]
        public ActionResult GetComments([FromQuery]PaginationQuery paginationQuery)
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex);
            }
        }

        // GET: api/Comments/5
        [HttpGet(ApiRoutes.Comments.Get)]
        public ActionResult GetComment(int id)
        {
            var comments = _repository.GetAll();


            var comment = comments.Where(c => id == c.BlogId).ToList();

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // GET: api/Comments/5/2
        [HttpGet(ApiRoutes.Comments.GetComment)]
        public ActionResult GetComment(int blogId, int id)
        {
            var comments = _repository.GetAll();


            var comment = comments.Where(c => blogId == c.BlogId).ToList();

            var singleComment = comment.Where(c => id == c.Id);

            if (singleComment == null)
            {
                return NotFound();
            }

            return Ok(singleComment);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut(ApiRoutes.Comments.Put)]
        public IActionResult PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            var oldComment = _repository.Get(comment.Id);
            if (oldComment == null) return NotFound($"Could not find blog with id of {comment.Id}");

            try
            {
                _repository.Update(comment);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost(ApiRoutes.Comments.Create)]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {

            try
            {
                var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name); //.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userMgr.FindByEmailAsync(userId.Value);

                if (user == null)
                {
                    return NotFound("User not found");
                }
                comment.UserId = user.Id;
                comment.DateOfComment = DateTime.Now;

                _repository.Add(comment);

                var result = await _repository.Commit();

                if (result)
                {
                    return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
                }

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex);
            }

            return BadRequest();

        }

        // DELETE: api/Comments/5
        [HttpDelete(ApiRoutes.Comments.Delete)]
        public ActionResult<Comment> DeleteComment(int id)
        {
            var comment = _repository.Get(id);
            if (comment == null)
            {
                return NotFound();
            }

            _repository.Delete(id);

            return NoContent();
        }

    }
}
