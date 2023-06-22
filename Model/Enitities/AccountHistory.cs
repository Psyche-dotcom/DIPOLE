using Model.Enum;

namespace Model.Enitities
{
    public class AccountHistory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Time { get; set; }
        public TransactionType TransactionType { get; set; }
        public Account Account { get; set; }
        public string  AccountId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
