using CloudinaryDotNet.Actions;

namespace DipoleBank.Service.Interface
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhoto(IFormFile file, object id);
    }
}
