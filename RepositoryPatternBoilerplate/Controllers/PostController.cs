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

        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <returns>All posts</returns>
        /// <response code="200">Returns all existing posts</response>
        // GET: api/<PostController>
        [HttpGet(Name = "GetPosts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAll().ToList());
        }

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A post containing given id</returns>
        /// <response code="200">Returns the post with given id</response>
        /// <response code="404">If the post doen't exist</response>
        // GET api/<PostController>/5
        [HttpGet("{id}", Name = "GetPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Gets all posts by given id
        /// </summary>
        /// <param name="blogId"></param>
        /// <returns>All posts containing given blogId</returns>
        /// <response code="200">Returns the post with given id</response>
        [HttpGet("getPostsByBlogId/{blogId}", Name = "GetPostByBlogId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPostByBlogId(int blogId)
        {
            return Ok(_postService.GetPostsByBlogId(blogId));
        }

        /// <summary>
        /// Creates a Post.
        /// </summary>
        /// <param name="post"></param>
        /// <returns>A newly created post</returns>
        /// <response code="200">Returns the newly created post</response>
        /// <response code="400">If the post is null</response>
        // POST api/<PostController>
        [HttpPost(Name = "SubmitPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Modifies a post by id
        /// </summary>
        /// /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>The modified post containing given id</returns>
        /// <response code="200">Returns the modified post with given id</response>
        /// <response code="400">If post or id are null</response>
        /// <response code="404">If the post doen't exist</response>
        // PUT api/<PostController>/5
        [HttpPut("{id}", Name = "PutPost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The deleted post containing given id</returns>
        /// <response code="200">Returns the deleted post with given id</response>
        /// <response code="400">If id is null</response>
        /// <response code="404">If the post doen't exist</response>
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
