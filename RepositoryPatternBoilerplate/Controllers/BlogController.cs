using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryPatternBoilerplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;

        private readonly IRepository<Blog> _blogRepository;

        public BlogController(ILogger<BlogController> logger, IRepository<Blog> blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        /// <summary>
        /// Gets all blogs
        /// </summary>
        /// <returns>All blogs</returns>
        // GET: api/<BlogController>
        [HttpGet(Name = "GetBlogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_blogRepository.GetAll().ToList());
        }

        /// <summary>
        /// Gets a blog by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A blog containing given id</returns>
        /// <response code="200">Returns the blog with given id</response>
        /// <response code="404">If the blog doen't exist</response>
        // GET: api/<BlogController>/5
        [HttpGet("{id}", Name = "GetBlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                Blog blog = _blogRepository.GetById(id);
                return Ok(blog);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Blog with id {id} not found");
            }
        }

        /// <summary>
        /// Creates a Blog.
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>A newly created Blog</returns>
        /// <response code="200">Returns the newly created blog</response>
        /// <response code="400">If the blog is null</response>
        // POST: api/<BlogController>
        [HttpPost(Name = "PostBlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Blog blog)
        {
            try
            {
                _blogRepository.Add(blog);
                _blogRepository.SaveChanges();
                return Ok(blog);
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Debug, e.Message, blog);
                return BadRequest("Could not add post");
            }
        }

        /// <summary>
        /// Modifies a blog by id
        /// </summary>
        /// /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>The modified blog containing given id</returns>
        /// <response code="200">Returns the modified blog with given id</response>
        /// <response code="400">If blog or id are null</response>
        /// <response code="404">If the blog doen't exist</response>
        // PUT api/<BlogController>/5
        [HttpPut("{id}", Name = "PutBlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Blog item)
        {
            try
            {
                Blog blog = _blogRepository.GetById(id);

                blog.Url = item.Url;

                _blogRepository.Update(blog);
                _blogRepository.SaveChanges();

                return Ok(blog);
            }
            catch(KeyNotFoundException)
            {
                return NotFound($"Blog with id {id} not found");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// Deletes a blog by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted blog containing given id</returns>
        /// <response code="200">Returns the deleted blog with given id</response>
        /// <response code="400">If id is null</response>
        /// <response code="404">If the blog doen't exist</response>
        // DELETE api/<BlogController>/5
        [HttpDelete("{id}", Name = "DeleteBlog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                Blog blog = _blogRepository.GetById(id);

                _blogRepository.Remove(blog);
                _blogRepository.SaveChanges();

                return Ok(blog);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Blog with id {id} not found");
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
