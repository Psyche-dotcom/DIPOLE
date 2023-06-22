using AutoMapper;
using CloudinaryDotNet;
using Data.Repository.Interface;
using DipoleBank.Service.Interface;
using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _accountRepo;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IGenerateJwt _generateJwt;
        private readonly ICloudinaryService _cloudinary;
        private readonly IEmailServices _emailServices;

        public UserService(IUserRepo accountRepo, ILogger<UserService> logger, IMapper mapper, IGenerateJwt generateJwt, ICloudinaryService cloudinary, IEmailServices emailServices)
        {
            _accountRepo = accountRepo;
            _logger = logger;
            _mapper = mapper;
            _generateJwt = generateJwt;
            _cloudinary = cloudinary;
            _emailServices = emailServices;
        }
        public async Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _accountRepo.FindUserByEmailAsync(signUp.Email);
                if (checkUserExist != null)
                {
                    response.ErrorMessages = new List<string>() { "User with the email already exist" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkRole = await _accountRepo.RoleExist(Role);
                if (checkRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Role is not available" };
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mapAccount = _mapper.Map<ApplicationUser>(signUp);
                mapAccount.UserName = mapAccount.AppId;
                var createUser = await _accountRepo.SignUpAsync(mapAccount, signUp.AppKey);
                if (createUser == null)
                {
                    response.ErrorMessages = new List<string>() { "User not created successfully" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var addRole = await _accountRepo.AddRoleAsync(createUser, Role);
                if (addRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Fail to add role to user" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                _emailServices.SendEmail(new Message(new string[] { signUp.Email }, "Account Details", $"<p>Your Account Details Below</p><br/><p>AppId: <strong>{createUser.AppId}</strong></p><p>AppKey: <strong>{signUp.AppKey}</strong></p>"));
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "User successfully created";
                response.Result = $"User registration details have been sent to {signUp.Email}";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in resgistering the user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;

            }
        }
        public async Task<ResponseDto<string>> LoginUser(SignInModel signIn)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkUserExist = await _accountRepo.CheckAppId(signIn.AppId);
                if (checkUserExist == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no user with the AppId provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkPassword = await _accountRepo.CheckAccountPassword(checkUserExist, signIn.AppKey);
                if (checkPassword == false)
                {
                    response.ErrorMessages = new List<string>() { "Invalid Password" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var generateToken = await _generateJwt.GenerateToken(checkUserExist);
                if (generateToken == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating jwt for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }

                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successfully login";
                response.Result = generateToken;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in login the user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }

        public async Task<ResponseDto<ForgotAppKeyDto>> ForgotAppId(string AppId)
        {
            var response = new ResponseDto<ForgotAppKeyDto>();
            try
            {
                var checkUser = await _accountRepo.CheckAppId(AppId);
                if (checkUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid AppId" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;

                }
                var result = await _accountRepo.ForgotAppKey(checkUser);
                if (result == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in generating reset token for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.DisplayMessage = "Token generated Successfully";
                response.Result = new ForgotAppKeyDto(checkUser.Email, result);
                response.StatusCode = 200;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in generating reset token for user" };
                response.StatusCode = 501;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> ResetAppKey(ResetAppKeyDto resetPassword)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.CheckAppId(resetPassword.AppId);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid app id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var resetPasswordAsync = await _accountRepo.ResetAppKeyIdAsync(findUser, resetPassword);
                if (resetPasswordAsync == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid token" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                _emailServices.SendEmail(new Message(new string[] { findUser.Email }, "Reset Appkey Details", $"<p>Your Updated Account Details Below</p><br/><p>AppId: <strong>{findUser.AppId}</strong></p><p>AppKey: <strong>{resetPassword.AppKey}</strong></p>"));
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successfully reset user password";
                response.Result = $"Successfully sent updated user details to {findUser.Email}";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in reset user password" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> DeleteUser(string AppId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.CheckAppId(AppId);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid App Id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var deleteUser = await _accountRepo.DeleteUserByEmail(findUser);
                if (deleteUser == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in deleting user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully delete user";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in deleting user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> UpdateUser(string appid, UpdateUserDto updateUser)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.CheckAppId(appid);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "There is no user with the email provided" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var mapUpdateDetails = _mapper.Map(updateUser, findUser);
                var updateUserDetails = await _accountRepo.UpdateUserInfo(mapUpdateDetails);
                if (updateUserDetails == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in updating user info" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully update user information";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in updating user info" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> UploadUserProfilePicture(string appid, IFormFile file)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.CheckAppId(appid);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "Invalid App Id" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var uploadImage = await _cloudinary.UploadPhoto(file, appid);
                if (uploadImage == null)
                {
                    response.ErrorMessages = new List<string>() { "Error in uploading profile picture for user" };
                    response.StatusCode = 501;
                    response.DisplayMessage = "Error";
                    return response;
                }
                findUser.ProfilePicture = uploadImage.Url.ToString();
                var updateUserDetails = await _accountRepo.UpdateUserInfo(findUser);
                if (updateUserDetails == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in updating user profile pictures" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Success";
                response.Result = "Successfully update user profile picture";
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in updating user info" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> UpdateUserRole(string appid, string role)
        {
            var response = new ResponseDto<string>();
            try
            {
                var findUser = await _accountRepo.CheckAppId(appid);
                if (findUser == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid AppId for first user" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var checkRole = await _accountRepo.RoleExist(role);
                if (checkRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Role is not available" };
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var getExistingRoles = await _accountRepo.GetUserRoles(findUser);
                if (getExistingRoles.Count == 0)
                {
                    response.ErrorMessages = new List<string>() { "There is no role for this user" };
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var removeExistingRoles = await _accountRepo.RemoveRoleAsync(findUser, getExistingRoles);
                if (removeExistingRoles == false)
                {
                    response.ErrorMessages = new List<string>() { "Error in removing role for user" };
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var addRole = await _accountRepo.AddRoleAsync(findUser, role);
                if (addRole == false)
                {
                    response.ErrorMessages = new List<string>() { "Fail to add role to user" };
                    response.StatusCode = StatusCodes.Status501NotImplemented;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "User role updated successfully";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in updating user role" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> AttachUser(string appId, string appId2)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkAppId = await _accountRepo.CheckAppId(appId);
                if (checkAppId == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid AppId for first user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkAppId.FollowerId != null)
                {
                    response.ErrorMessages = new List<string>() { "First user is already attach to another user" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var checkAppId2 = await _accountRepo.CheckAppId(appId2);
                if (checkAppId2 == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid AppId for second user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkAppId2.FollowerId != null)
                {
                    response.ErrorMessages = new List<string>() { "Second user is already attach to another user" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                var attachUser = await _accountRepo.attachedUser(checkAppId, checkAppId2);
                if(attachUser == false)
                {
                    response.ErrorMessages = new List<string>() { "User not attach successfully" };
                    response.StatusCode = 500;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "user attached together successfully";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in attaching user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<string>> DettachUser(string appId)
        {
            var response = new ResponseDto<string>();
            try
            {
                var checkAppId = await _accountRepo.CheckAppId(appId);
                if (checkAppId == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid AppId for user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                if (checkAppId.FollowerId == null)
                {
                    response.ErrorMessages = new List<string>() { "Cannot detach unattach user" };
                    response.DisplayMessage = "Error";
                    response.StatusCode = 400;
                    return response;
                }
                
                var dettachUser = await _accountRepo.DettachedUser(checkAppId);
                if (dettachUser == false)
                {
                    response.ErrorMessages = new List<string>() { "User not dettach successfully" };
                    response.StatusCode = 500;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = "user detached together successfully";
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in detaching user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
        }
        public async Task<ResponseDto<PaginatedUser>> GetAllUser(int pageNumber, int perPageSize, string AppId)
        {
            var response = new ResponseDto<PaginatedUser>();
            try
            {
                var user = await _accountRepo.GetAllUser(pageNumber, perPageSize, AppId);
                if (!user.User.Any())
                {
                    response.ErrorMessages = new List<string>() { "User not available" };
                    response.StatusCode = StatusCodes.Status404NotFound;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = user;
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting user details" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }

        }

        public async Task<ResponseDto<DisplayFindUserDTO>> GetAttachUser(string AppId)
        {
            var response = new ResponseDto<DisplayFindUserDTO>();
            try
            {
                var checkAppId = await _accountRepo.CheckAppId(AppId);
                if (checkAppId == null)
                {
                    response.ErrorMessages = new List<string>() { "invalid AppId for user" };
                    response.StatusCode = 400;
                    response.DisplayMessage = "Error";
                    return response;
                }
                var getAttachUser = await _accountRepo.GetAttachUser(AppId);
                if (getAttachUser == null)
                {
                    response.ErrorMessages = new List<string>() { "No user is attach to this AppId" };
                    response.StatusCode = 404;
                    response.DisplayMessage = "Error";
                    return response;
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.DisplayMessage = "Successful";
                response.Result = getAttachUser;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                response.ErrorMessages = new List<string>() { "Error in getting attached user" };
                response.StatusCode = 500;
                response.DisplayMessage = "Error";
                return response;
            }
            
        }
    }
}
