using Model.Enitities;

namespace Data.Repository.Interface
{
    public interface IBankRepo
    {
        Task<Bank> Createbank(Bank bank);
    }
}
