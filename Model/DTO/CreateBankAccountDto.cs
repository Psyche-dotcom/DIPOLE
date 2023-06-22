using Model.Enum;

namespace Model.DTO
{
    public class CreateBankAccountDto
    {
        public AccountType AccountType { get; set; }
        public string AppId { get; set; }
    }
}
