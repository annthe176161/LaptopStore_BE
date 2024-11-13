using LaptopStore.Business.DTOs;
using LaptopStore.Business.Interfaces;
using LaptopStore.Data.Repositories;

namespace LaptopStore.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<byte[]> ExportToExcelAsync(IEnumerable<UserDto> users)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetInactiveUsers(DateTime since)
        {
            var users = await _userRepository.FilterInactiveUsers(since);
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive,
                LastLoginDate = u.LastLoginDate
            });
        }

        public async Task<IEnumerable<UserDto>> GetPagedUsersAsync(int page, int pageSize)
        {
            var users = await _userRepository.GetUsersAsync(page, pageSize);
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive,
                LastLoginDate = u.LastLoginDate
            });
        }

        public Task SendReminderEmailsAsync(IEnumerable<UserDto> users)
        {
            throw new NotImplementedException();
        }

        public async Task ToggleUserAccessAsync(string userId, bool isActive)
        {
            await _userRepository.ToggleUserAccessAsync(userId, isActive);
        }

        public async Task UpdateUserRoleAsync(string userId, string role)
        {
            await _userRepository.UpdateUserRoleAsync(userId, role);
        }
    }
}
