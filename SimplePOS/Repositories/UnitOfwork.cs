using SimplePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Repositories
{
    public interface IUnitOfWork
    {
       void SaveEntities(Account account, Transaction trans);
    }
    public class UnitOfWork: IUnitOfWork
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transRepository;
        private SimplePOSDbContext _context;
        public UnitOfWork(IAccountRepository repository, ITransactionRepository transRepository,
           SimplePOSDbContext context)
        {
            _accountRepository = repository;
            _transRepository = transRepository;           
            _context = context;
        }

        public void SaveEntities(Account account, Transaction trans)
        {
            /*
             * System.InvalidOperationException: 'An error was generated for warning 'Microsoft.EntityFrameworkCore.Database.Transaction.TransactionIgnoredWarning': Transactions are not supported by the in-memory store. See http://go.microsoft.com/fwlink/?LinkId=800142 This exception can be suppressed or logged by passing event ID 'InMemoryEventId.TransactionIgnoredWarning' to the 'ConfigureWarnings' method in 'DbContext.OnConfiguring' or 'AddDbContext'.'
             * */

            //using (var DbTransection = _context.Database.BeginTransaction())
            //{
                account = _accountRepository.Save(account);
                trans = _transRepository.Save(trans);
               // DbTransection.Commit();
            //}
        }
    }
}
