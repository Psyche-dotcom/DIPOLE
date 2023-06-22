using Model.DTO;

namespace DipoleBank.Service.Interface
{
    public interface IEmailServices
    {
        void SendEmail(Message message);
    }
}
