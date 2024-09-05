using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet("getPosts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }

        [HttpPost("addPost")]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            await _postService.AddPostAsync(post);
            return Ok(new { message = "Post added successfully" });
        }

        [HttpPost("deletePost")]
        public async Task<IActionResult> DeletePost([FromBody] int Id)
        {
            await _postService.DeletePostAsync(Id);
            return Ok(new { message = "Post deleted successfully" });
        }
    }
}
