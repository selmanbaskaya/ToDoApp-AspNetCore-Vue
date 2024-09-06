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
            var formattedPost = new Post
            {
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId
            };

            await _postService.AddPostAsync(formattedPost);
            return Ok(new { message = "Post added successfully" });
        }

        [HttpPost("deletePost")]
        public async Task<IActionResult> DeletePost([FromBody] Post post)
        {
            await _postService.DeletePostAsync(post.Id);
            return Ok(new { message = "Post deleted successfully" });
        }
    }
}
