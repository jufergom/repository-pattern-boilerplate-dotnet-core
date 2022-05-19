using Boilerplate.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepositoryPatternBoilerplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;

        private readonly IRepository<Post> _postRepository;

        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IRepository<Post> postRepository, IPostService postService)
        {
            _logger = logger;
            _postRepository = postRepository;
            _postService = postService;
        }

        // GET: api/<PostController>
        [HttpGet(Name = "GetPosts")]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAll().ToList());
        }

        // GET api/<PostController>/5
        [HttpGet("{id}", Name = "GetPost")]
        public IActionResult Get(int id)
        {
            try
            {
                Post post = _postRepository.GetById(id);
                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Post with id {id} not found");
            }
        }

        [HttpGet("{blogId}", Name = "GetPostByBlogId")]
        public IActionResult GetPostByBlogId(int blogId)
        {
            return Ok(_postService.GetPostsByBlogId(blogId));
        }

        // POST api/<PostController>
        [HttpPost(Name = "SubmitPost")]
        public IActionResult Post([FromBody] Post post)
        {
            try
            {
                _postRepository.Add(post);
                _postRepository.SaveChanges();
                return Ok(post);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Debug, e.Message, post);
                return BadRequest("Could not add post");
            }
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}", Name = "PutPost")]
        public IActionResult Put(int id, [FromBody] Post item)
        {
            try
            {
                Post post = _postRepository.GetById(id);

                post.Title = item.Title;
                post.Content = item.Content;

                _postRepository.Update(post);
                _postRepository.SaveChanges();

                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Post with id {id} not found");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}", Name = "PutPost")]
        public IActionResult Delete(int id)
        {
            try
            {
                Post post = _postRepository.GetById(id);

                _postRepository.Remove(post);
                _postRepository.SaveChanges();

                return Ok(post);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Post with id {id} not found");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
