using LaptopStore.Data.Entities;

namespace LaptopStore.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync(int page, int pageSize);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task UpdateUserRoleAsync(string userId, string newRole);
        Task ToggleUserAccessAsync(string userId, bool isActive);
        Task<IEnumerable<ApplicationUser>> FilterInactiveUsers(DateTime since);
    }
}
