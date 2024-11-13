using LaptopStore.Data.Contexts;
using LaptopStore.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LaptopStore.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> FilterInactiveUsers(DateTime since)
        {
            return await _context.Users.Where(u => u.LastLoginDate < since && !u.IsActive).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync(int page, int pageSize)
        {
            return await _context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task ToggleUserAccessAsync(string userId, bool isActive)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsActive = isActive;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserRoleAsync(string userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, newRole);
            }
        }
    }
}
