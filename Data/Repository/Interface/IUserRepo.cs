using Model.DTO;
using Model.Enitities;

namespace Data.Repository.Interface
{
    public interface IUserRepo
    {
        Task<ApplicationUser> SignUpAsync(ApplicationUser user, string Password);
        Task<bool> CheckAccountPassword(ApplicationUser user, string password);
        Task<bool> CheckEmailConfirmed(ApplicationUser user);
        Task<bool> AddRoleAsync(ApplicationUser user, string Role);
        Task<string> ForgotAppKey(ApplicationUser user);
        Task<ApplicationUser> CheckAppId(string AppId);
        Task<bool> ConfirmEmail(string token, ApplicationUser user);
        Task<bool> RemoveRoleAsync(ApplicationUser user, IList<string> role);
        Task<ResetAppKeyDto> ResetAppKeyIdAsync(ApplicationUser user, ResetAppKeyDto resetPassword);
        Task<bool> RoleExist(string Role);
        Task<ApplicationUser?> FindUserByEmailAsync(string email);
        Task<ApplicationUser> FindUserByIdAsync(string id);
        Task<bool> UpdateUserInfo(ApplicationUser applicationUser);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
        Task<PaginatedUser> GetAllUser(int pageNumber, int perPageSize, string AppId);
        Task<bool> DeleteUserByEmail(ApplicationUser user);
        Task<bool> attachedUser(ApplicationUser checkUser, ApplicationUser checkUser2);
        Task<bool> ValidateAttachUser(string AppId);
        Task<bool> DettachedUser(ApplicationUser checkUser);
        Task<DisplayFindUserDTO> GetAttachUser(string AppId);
    }
}
