using SimplePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Repositories
{
    public interface IAccountRepository
    {
        Account GetById(long accountId);
        Account Save(Account account);
    }

    public class AccountRepository : IAccountRepository
    {

        //public static List<Account> Accounts = new List<Account>()
        //{
        //    new Account(4755, 1001.88m),
        //    new Account(9834, 456.45m),
        //    new Account(7735, 89.36m)
        //};

        private readonly SimplePOSDbContext _context;
        public AccountRepository(SimplePOSDbContext context)
        {
            _context = context;
        }


        public Account GetById(long accountId)
        {

            //Account account = Accounts.FirstOrDefault(a => a.AccountId == accountId);            
            Account account = _context.Accounts.Find(accountId);
            return account;
        }

        public Account Save(Account account)
        {
            //Accounts.Find(a => account.AccountId == account.AccountId).Balance = account.Balance;
            Account current = _context.Accounts.Find(account.AccountId);
            current.Balance = account.Balance;
            int count =_context.SaveChanges();
            return current;
        }

      
    }

  
}
