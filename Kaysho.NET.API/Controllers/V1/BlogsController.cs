using AutoMapper;
using DamilolaShopeyin.API.Data;
using DamilolaShopeyin.Core.Models;
using DamilolaShopeyin.Core.Services;
using Kaysho.NET.API.Controllers.V1;
using Kaysho.NET.API.Helpers;
using Kaysho.NET.API.Hubs;
using Kaysho.NET.API.Service;
using Kaysho.NET.Core.Constants;
using Kaysho.NET.Core.Contracts.V1;
using Kaysho.NET.Core.Contracts.V1.Requests;
using Kaysho.NET.Core.Contracts.V1.Requests.Queries;
using Kaysho.NET.Core.Contracts.V1.Responses;
using Kaysho.NET.Core.Dto;
using Kaysho.NET.Core.Models;
using Kaysho.NET.Core.V1.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DamilolaShopeyin.API.Controllers.V1
{
    [Authorize(Roles = RoleConstants.AdminRole)]
    [ApiController]
    [Produces("application/json")]
    public class BlogsController : BaseController
    {
        private readonly IUriService _uriService;
        private readonly IRepository<Blog> _repository;
        IHubContext<BlogHub> Hub;
        private readonly ICloudStorage _cloudStorage;
        public BlogsController(IUriService uriService, IHubContext<BlogHub> hub, IRepository<Blog> blogRepository, UserManager<ApplicationUser> userMgr, IHttpContextAccessor httpContextAccessor, IMapper mapper, ICloudStorage cloudStorage) : base(userMgr, httpContextAccessor, mapper)
        {
            this._repository = blogRepository;
            Hub = hub;
            _cloudStorage = cloudStorage;
            _uriService = uriService;
        }

        // GET: api/v1/Blogs
        /// <summary>
        /// Returns all the blogs in the system
        /// </summary>
        /// <response code="200">Returns all the blogs in the system</response>
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Blogs.GetAll)]
        public ActionResult<IEnumerable<Blog>> GetBlogs([FromQuery] GetAllBlogsQuery query, [FromQuery]PaginationQuery paginationQuery)
        {
            try
            {
                var pagination = mapper.Map<PaginationFilter>(paginationQuery);
                var filter = mapper.Map<GetAllBlogsFilter>(query);
                var blogs = _repository.GetAll(pagination).Where(b => b.IsDeleted == false).ToList();

                var blogsResponse = mapper.Map<List<BlogResponse>>(blogs);

                if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
                {
                    return Ok(new PagedResponse<BlogResponse>(blogsResponse));
                }

                if (!string.IsNullOrWhiteSpace(filter.BlogTitle))
                {
                    return Ok(new Response<IEnumerable<Blog>>
                    {
                        Message = "Blogs retrieved successfully",
                        Data = _repository.GetAll().Where(b => b.Title.Contains(filter.BlogTitle)).ToList()
                    });
                }

                var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, blogsResponse);
                return Ok(paginationResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        // GET: api/Blogs/5
        /// <summary>
        /// Returns a particlar blog in the system
        /// </summary>
        /// <response code="200">Returns a particlar blog in the system</response>
        [AllowAnonymous]
        [HttpGet(ApiRoutes.Blogs.Get)]
        public ActionResult<Blog> GetBlog(int id)
        {
            var blog = _repository.Get(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(new Response<BlogResponse>
            {
                Data = mapper.Map<BlogResponse>(blog),
                Message = "Blog retrieved successfully",
                Error = false
            });
        }

        // PUT: api/Blogs/5
        /// <summary>
        /// Update a particlar blog in the system
        /// </summary>
        /// <response code="204">Returns bothing from the system</response>
        [HttpPut(ApiRoutes.Blogs.Put)]
        public async Task<IActionResult> PutBlog(int id, UpdateBlogRequest updateBlogRequest)
        {

            if (id != updateBlogRequest.Id)
            {
                return BadRequest(new ErrorResponse(new ErrorModel { Message = "Invalid operation" }));
            }

            var oldBlog = _repository.Get(updateBlogRequest.Id);
            if (oldBlog == null) return NotFound($"Could not find blog with id of {updateBlogRequest.Id}");

            //var blog = new Blog
            //{
            //    Id = updateBlogRequest.Id,
            //    UpdatedAt = DateTime.Now,
            //    Article = updateBlogRequest.Article,
            //    Title = updateBlogRequest.Title,
            //    CreatedAt = oldBlog.CreatedAt

            //};

            oldBlog.Title = updateBlogRequest.Title;
            oldBlog.Article = updateBlogRequest.Article;
            oldBlog.UpdatedAt = DateTime.Now;

            try
            {

                _repository.Update(oldBlog);
                var result = await _repository.Commit();

                if (result)
                {

                }
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }

        // POST: api/Blogs
        /// <summary>
        /// Creates a blog in the system
        /// </summary>
        /// <response code="201">Creates a blog in the system</response>
        /// <response code="400">Unable to create the blog due to validation error</response>
        [HttpPost(ApiRoutes.Blogs.Create)]
        [ProducesResponseType(typeof(BlogResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> PostBlog(CreateBlogRequest createBlogRequest)
        {
            try
            {
                var blog = new Blog
                {
                    Title = createBlogRequest.Title,
                    Article = createBlogRequest.Article,
                    CreatedAt = DateTime.Now
                };
                var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name); //.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userMgr.FindByEmailAsync(userId.Value);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                blog.User = user.Id;
                _repository.Add(blog);

                var result = await _repository.Commit();

                await Hub.Clients.All.SendAsync("NewBlog", JsonConvert.SerializeObject(blog));

                if (result)
                {
                    var locationUri = _uriService.GetBlogUri(blog.Id.ToString());
                    return Created(locationUri, new Response<BlogResponse>
                    {
                        Message = "Blog created successfully",
                        Error = false,
                        Data = (mapper.Map<BlogResponse>(blog))
                    });

                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return BadRequest();

        }

        // DELETE: api/Blogs/5
        /// <summary>
        /// Deletes a particlar blog in the system
        /// </summary>
        /// <response code="204">Returns no content</response>
        /// <response code="404">Returns resource not found</response>
        [HttpDelete(ApiRoutes.Blogs.Delete)]
        public async Task<ActionResult<Blog>> DeleteBlog(int id)
        {
            var blog = _repository.Get(id);
            if (blog == null)
            {
                return NotFound();
            }

            if (blog.ImageStorageName != null)
            {

                await _cloudStorage.DeleteFileAsync(blog.ImageStorageName);
            }

            blog.IsDeleted = true;
            _repository.Update(blog);

            var result = await _repository.Commit();



            return NoContent();
        }

        /// <summary>
        /// Upload image for a particlar blog in the system
        /// </summary>
        /// <response code="200">Returns success with empty body</response>
        /// <response code="404">Returns resource not found</response>
        [HttpPost("image/{id}")]
        public async Task<ActionResult> UploadImage(int id, [FromForm]ImageUpload formFile)
        {
            var blog = _repository.Get(id);
            if (blog == null)
            {
                return NotFound();
            }

            if (blog.IsDeleted)
            {
                return NotFound();
            }

            if (formFile.Avatar != null)
            {
                blog.ImageFile = formFile.Avatar;
                await UploadFile(blog);

                return Ok();

            }

            return BadRequest();
        }

        private async Task UploadFile(Blog blog)
        {
            string fileNameForStorage = FormFileName(blog.Title, blog.ImageFile.FileName);
            blog.ImageUrl = await _cloudStorage.UploadFileAsync(blog.ImageFile, fileNameForStorage);
            blog.ImageStorageName = fileNameForStorage;
            _repository.Update(blog);

            var result = await _repository.Commit();
        }

        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }
    }
}
