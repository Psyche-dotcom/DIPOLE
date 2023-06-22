using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Service.Interface
{
    public interface IBankAccountService
    {
        Task<ResponseDto<DisplayAccountDto>> CreateBankAccount(CreateBankAccountDto account);
    }
}
