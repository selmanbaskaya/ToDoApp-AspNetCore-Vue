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
            try
            {
                var posts = await _postService.GetPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving posts.", ex);
            }
        }

        [HttpPost("addPost")]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the post.", ex);
            }
        }

        [HttpPost("deletePost")]
        public async Task<IActionResult> DeletePost([FromBody] Post post)
        {
            try
            {
                await _postService.DeletePostAsync(post.Id);
                return Ok(new { message = "Post deleted successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the post.", ex);
            }
        }
    }
}
