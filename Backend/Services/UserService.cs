using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                user.CreatedDate = DateTime.UtcNow;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = _passwordHasher.HashPassword(user, user.Password);
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the users.", ex);
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }
    }
}
