using Model.DTO;
using Model.Enitities;

namespace DipoleBank.Service.Interface
{
    public interface IBankService
    {
        Task<ResponseDto<Bank>> CreateBank(CreateBankDto bank);
    }
}
