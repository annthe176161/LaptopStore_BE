using LaptopStore.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedUsers(int page = 1, int pageSize = 20)
        {
            var users = await _userService.GetPagedUsersAsync(page, pageSize);
            return Ok(users);
        }

        [HttpPost("toggle-access")]
        public async Task<IActionResult> ToggleUserAccess([FromBody] string userId, bool isActive)
        {
            await _userService.ToggleUserAccessAsync(userId, isActive);
            return Ok();
        }

        [HttpPost("update-role")]
        public async Task<IActionResult> UpdateUserRole([FromBody] string userId, string role)
        {
            await _userService.UpdateUserRoleAsync(userId, role);
            return Ok();
        }
    }
}
