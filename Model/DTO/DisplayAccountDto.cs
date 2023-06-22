using Model.Enitities;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DisplayAccountDto
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public long AccountBalance { get; set; } 
        public DateTime DateCreated { get; set; }
        public AccountType AccountType { get; set; }
        public ICollection<AccountHistory> AccountHistories { get; set; }
        public string BankName { get; set; } = "DipoleDiamond";
    }
}
