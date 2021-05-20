using SimplePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Repositories
{
    public interface ITransactionRepository
    {
        Transaction GetById(string accountId);
        Transaction Save(Transaction account);
    }

    public class TransactionRepository : ITransactionRepository
    {

        //public static List<Account> Accounts = new List<Account>()
        //{
        //    new Account(4755, 1001.88m),
        //    new Account(9834, 456.45m),
        //    new Account(7735, 89.36m)
        //};

        private readonly SimplePOSDbContext _context;
        public TransactionRepository(SimplePOSDbContext context)
        {
            _context = context;
        }


        public Transaction GetById(string id)
        {
            Transaction trans = _context.Transactions.Find(id);
            return trans;
        }

        public Transaction Save(Transaction trans)
        {
            Transaction currentTrans = _context.Transactions.FirstOrDefault(t => t.TransactionId == trans.TransactionId);
            if (currentTrans == null)
            {
                _context.Transactions.Add(trans);
            }
            else
            {
                currentTrans = trans;
            }
            int count =_context.SaveChanges();
            return trans;
        }
    }

  
}
