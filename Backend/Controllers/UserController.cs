using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                var newUser = await _userService.AddUser(user);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);    
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);    
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the users.", ex);
            }
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the user by id.", ex);
            }
        }

        [HttpPost("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] User user)
        {
            try
            {
                await _userService.DeleteUser(user.Id);
                return Ok(new { message = "User deleted successfully" });    
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }
    }
}
