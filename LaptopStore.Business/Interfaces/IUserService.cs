using LaptopStore.Business.DTOs;

namespace LaptopStore.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetPagedUsersAsync(int page, int pageSize);
        Task ToggleUserAccessAsync(string userId, bool isActive);
        Task UpdateUserRoleAsync(string userId, string role);
        Task<IEnumerable<UserDto>> GetInactiveUsers(DateTime since);
        Task<byte[]> ExportToExcelAsync(IEnumerable<UserDto> users);
        Task SendReminderEmailsAsync(IEnumerable<UserDto> users);
    }
}
