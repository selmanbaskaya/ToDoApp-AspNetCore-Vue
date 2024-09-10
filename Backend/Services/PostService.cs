using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class PostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            try
            {
                return await _context.Posts.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving posts.", ex);
            }
        }

        public async Task AddPostAsync(Post post)
        {
            try
            {
                post.CreatedDate = DateTime.UtcNow;
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the post.", ex);
            }
        }

        public async Task DeletePostAsync(int Id)
        {
            try
            {
                var post = await _context.Posts.FindAsync(Id);
                if (post != null)
                {
                    _context.Posts.Remove(post);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the post.", ex);
            }
        }
    }
}
