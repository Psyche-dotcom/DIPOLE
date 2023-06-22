using Data.Context;
using Data.Repository.Interface;
using Model.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementation
{
    public class BankAccountRepo : IBankAccountRepo
    {
        private readonly DipoleBankContext _context;

        public BankAccountRepo(DipoleBankContext context)
        {
            _context = context;
        }
        public async Task<Account> CreateAccount(Account account)
        {
            var createAccount = await _context.Accounts.AddAsync(account);
            var save = await _context.SaveChangesAsync();
            if (save > 0)
            {
                return createAccount.Entity;
            }
            return null;
        }
    }
}
