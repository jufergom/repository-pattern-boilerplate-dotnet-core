using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepositoryPatternBoilerplate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IRepository<Blog> _blogRepository;

        public BlogController(ILogger<WeatherForecastController> logger, IRepository<Blog> blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        [HttpGet(Name = "GetBlog")]
        public IActionResult Get()
        {
            return Ok(_blogRepository.GetAll().ToList());
        }

        [HttpGet("{id}", Name = "GetBlogs")]
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

        [HttpPut(Name = "PutBlog")]
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

        [HttpPut(Name = "DeleteBlog")]
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
