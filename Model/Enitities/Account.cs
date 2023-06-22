using Model.Enum;

namespace Model.Enitities
{
    using System;

    public class Account
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string AccountNumber { get; set; }
        public long AccountBalance { get; set; } = 0;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public AccountType AccountType { get; set; }
        public ICollection<AccountHistory> AccountHistories { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string BankId { get; set; } = "74eb4e7c-b2af-494c-b624-4a959eb0811f";
        public Bank Bank { get; set; }

        public Account()
        {
            AccountNumber = GenerateAccountNumber();
        }

        private string GenerateAccountNumber()
        {
            string prefix = "34550";
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            string uniqueIdentifier = randomNumber.ToString();
            return prefix + uniqueIdentifier;
        }
    }

}
