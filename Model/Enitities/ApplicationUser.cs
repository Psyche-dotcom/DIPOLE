using Microsoft.AspNetCore.Identity;
using Model.Enum;
using System.Diagnostics.Metrics;

namespace Model.Enitities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AppId { get; set; } = Guid.NewGuid().ToString();
        public Gender Gender { get; set; }
        public string ProfilePicture { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string? FollowerId { get; set; }
        public  ApplicationUser Follower { get; set; }
        public ICollection<BankUser> BankUsers { get; set; }
        public ICollection<AccountHistory> AccountHistories { get; set; }
        public ICollection<Account> Accounts { get; set; }
        
    }
}
