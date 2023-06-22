using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enitities
{
    public class Bank
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<BankUser> BankUsers { get; set; } 
       
    }
}
