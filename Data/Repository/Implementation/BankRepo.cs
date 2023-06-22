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
    public class BankRepo : IBankRepo
    {
        private readonly DipoleBankContext _context;

        public BankRepo(DipoleBankContext context)
        {
            _context = context;
        }

        public async Task<Bank> Createbank(Bank bank)
        {
            var createbank = await _context.Banks.AddAsync(bank);
            var save= await _context.SaveChangesAsync();
            if(save> 0)
            {
                return createbank.Entity;
            }
            return null;
        }
    }
}
