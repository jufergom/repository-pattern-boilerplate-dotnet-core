using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryPatternBoilerplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;

        private readonly IRepository<Blog> _blogRepository;

        public BlogController(ILogger<BlogController> logger, IRepository<Blog> blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        // GET: api/<BlogController>
        [HttpGet(Name = "GetBlogs")]
        public IActionResult Get()
        {
            return Ok(_blogRepository.GetAll().ToList());
        }

        // GET: api/<BlogController>/5
        [HttpGet("{id}", Name = "GetBlog")]
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

        // POST: api/<BlogController>
        [HttpPost(Name = "PostBlog")]
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

        // PUT api/<BlogController>/5
        [HttpPut("{id}", Name = "PutBlog")]
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

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}", Name = "DeleteBlog")]
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
