using Model.Enitities;

namespace DipoleBank.Service.Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
