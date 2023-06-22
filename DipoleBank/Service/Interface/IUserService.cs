using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Service.Interface
{
    public interface IUserService
    {
        Task<ResponseDto<string>> RegisterUser(SignUp signUp, string Role);
        Task<ResponseDto<string>> LoginUser(SignInModel signIn);
        Task<ResponseDto<ForgotAppKeyDto>> ForgotAppId(string AppId);
        Task<ResponseDto<string>> ResetAppKey(ResetAppKeyDto resetPassword);
        Task<ResponseDto<PaginatedUser>> GetAllUser(int pageNumber, int perPageSize, string AppId);
        Task<ResponseDto<string>> DeleteUser(string AppId);
        Task<ResponseDto<string>> UpdateUser(string appid, UpdateUserDto updateUser);
        Task<ResponseDto<string>> UploadUserProfilePicture(string appid, IFormFile file);
        Task<ResponseDto<string>> UpdateUserRole(string appid, string role);
        Task<ResponseDto<string>> AttachUser(string appId, string appId2);
        Task<ResponseDto<string>> DettachUser(string appId);
        Task<ResponseDto<DisplayFindUserDTO>> GetAttachUser(string AppId);
    }
}
