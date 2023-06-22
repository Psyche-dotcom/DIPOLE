using Data.Context;
using Data.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Enitities;

namespace Data.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DipoleBankContext _context;

        public UserRepo(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, DipoleBankContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<bool> AddRoleAsync(ApplicationUser user, string Role)
        {
            var AddRole = await _userManager.AddToRoleAsync(user, Role);
            if (AddRole.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<ApplicationUser> CheckAppId(string AppId)
        {
            var getUser = await _context.Users.FirstOrDefaultAsync(u => u.AppId == AppId);
            if (getUser == null)
            {
                return null;
            }
            return getUser;
        }

        public async Task<bool> RemoveRoleAsync(ApplicationUser user, IList<string> role)
        {
            var removeRole = await _userManager.RemoveFromRolesAsync(user, role);
            if (removeRole.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            var getRoles = await _userManager.GetRolesAsync(user);
            if (getRoles != null)
            {
                return getRoles;
            }
            return null;
        }
        public async Task<bool> RoleExist(string Role)
        {
            var check = await _roleManager.RoleExistsAsync(Role);
            return check;
        }


        public async Task<bool> ConfirmEmail(string token, ApplicationUser user)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserByEmail(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<ApplicationUser?> FindUserByEmailAsync(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null)
            {
                return null;
            }
            return findUser;
        }

        public async Task<ApplicationUser> FindUserByIdAsync(string id)
        {
            var findUser = await _userManager.FindByIdAsync(id);
            return findUser;
        }

        public async Task<string> ForgotAppKey(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }
        public async Task<bool> ValidateAttachUser(string UserId)
        {
            var checkuser = await _context.Users.FirstOrDefaultAsync(u => u.FollowerId == UserId);
            if (checkuser == null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> attachedUser(ApplicationUser checkUser, ApplicationUser checkUser2)
        {
            checkUser.FollowerId = checkUser2.Id;
            checkUser2.FollowerId = checkUser.Id;
            _context.Users.Update(checkUser);
            _context.Users.Update(checkUser2);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DettachedUser(ApplicationUser checkUser)
        {
            checkUser.FollowerId = null;
            _context.Users.Update(checkUser);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<PaginatedUser> GetAllUser(int pageNumber, int perPageSize, string AppId)
        {
            var getAllUser = _userManager.Users;
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var totalCount = getAllUser.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var paginated = await getAllUser.Where(u => u.AppId != AppId).Skip((pageNumber - 1) * perPageSize).Take(perPageSize).Select(User => new DisplayFindUserDTO
            {
                AppId = User.AppId,
                Email = User.Email,
                FirstName = User.FirstName,
                LastName = User.LastName,
                PhoneNumber = User.PhoneNumber,
                ProfilePicture = User.ProfilePicture,
                Gender = User.Gender,
                Id = User.Id
            }).ToListAsync();

            var result = new PaginatedUser
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                User = paginated
            };
            return result;
        }
        public async Task<DisplayFindUserDTO> GetAttachUser(string AppId)
        {
            var getUser = await _context.Users.Include(e=>e.Follower).FirstOrDefaultAsync(u => u.AppId == AppId);
            var result = new DisplayFindUserDTO();
            if(getUser.Follower == null)
            {
                return null;
            }
            result.AppId = getUser.Follower.AppId;
            result.Email = getUser.Follower.Email;
            result.FirstName = getUser.Follower.FirstName;
            result.LastName = getUser.Follower.LastName;
            result.PhoneNumber = getUser.Follower.PhoneNumber;
            result.Gender = getUser.Follower.Gender;
            result.Id = getUser.Follower.Id;
            return result;
        }
        public async Task<bool> CheckEmailConfirmed(ApplicationUser user)
        {
            var checkConfirm = await _userManager.IsEmailConfirmedAsync(user);
            return checkConfirm;
        }
        public async Task<bool> CheckAccountPassword(ApplicationUser user, string password)
        {
            var checkUserPassword = await _userManager.CheckPasswordAsync(user, password);
            return checkUserPassword;
        }
        public async Task<ResetAppKeyDto> ResetAppKeyIdAsync(ApplicationUser user, ResetAppKeyDto resetPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.AppKey);
            if (result.Succeeded)
            {
                return resetPassword;
            }
            return null;
        }

        public async Task<ApplicationUser> SignUpAsync(ApplicationUser user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                return user;
            }
            return null;

        }

        public async Task<bool> UpdateUserInfo(ApplicationUser applicationUser)
        {
            var updateUserInfo = await _userManager.UpdateAsync(applicationUser);
            if (updateUserInfo.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
