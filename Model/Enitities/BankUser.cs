using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enitities
{
    public class BankUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BankId { get; set; }
        public Bank Bank { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
    }
}
