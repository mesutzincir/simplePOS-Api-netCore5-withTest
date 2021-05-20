using SimplePOS.DTOs;
using SimplePOS.Models;
using SimplePOS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Services
{

    public interface IAccountService
    {
        Account GetAccountById(long id);
        decimal SaveTransaction(Transaction trans);
        void ValidateNewBalance(decimal newBalance);
    }

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
       private readonly ITransactionRepository _transRepository;
        private readonly ICommissionFactory _commissionFactory;
        private readonly IUnitOfWork _unitOfwork;

        public AccountService(IAccountRepository repository, ITransactionRepository transRepository, 
            ICommissionFactory commissionFactory, IUnitOfWork unitOfwork)
        {
            _accountRepository = repository;
            _transRepository = transRepository;
            _commissionFactory = commissionFactory;
            _unitOfwork = unitOfwork;
        }
       
        public Account GetAccountById(long id)
        {
            return  _accountRepository.GetById(id);
        }

        public decimal SaveTransaction(Transaction trans)
        {           
            Account account = null;
            if(Transaction.PAYMENT.Equals(trans.MessageType))
            {
                account= MakePayment(trans);
            }
            else if (Transaction.ADJUSTMENT.Equals(trans.MessageType))
            {
                account = MakeAdjustment(trans);
            }           
            
            return account.Balance;
        }


        public Account MakeAdjustment(Transaction trans)
        {
            Account account = _accountRepository.GetById(trans.AccountId);
            if (account == null)
            {
                throw new Exception("account not found.");
            }

            Transaction oldTrans = _transRepository.GetById(trans.TransactionId);
            if(oldTrans == null)
            {
                throw new Exception("old payment transaction not found.");
            }
            trans.oldBalance = oldTrans.oldBalance;
            decimal commision = _commissionFactory.Create(trans.Origin).Evaluate(trans.Amount);
            trans.commision = commision;
            decimal adjustment = trans.Amount + commision;            
            decimal diffBalance = (oldTrans.newBalance-oldTrans.oldBalance) - adjustment;
            // add diference to balance.
            account.Balance = account.Balance + diffBalance;
            trans.newBalance = oldTrans.newBalance+ diffBalance;
            _unitOfwork.SaveEntities(account, trans);
            return account;
        }

        public Account MakePayment(Transaction trans)
        {
            Account account = _accountRepository.GetById(trans.AccountId);
            if (account == null)
            {
                throw new Exception("account not found.");
            }
            trans.oldBalance = account.Balance;
            decimal commision = _commissionFactory.Create(trans.Origin).Evaluate(trans.Amount);
            trans.commision = commision;
            account.Balance = evalNewBalance(account.Balance, trans.Amount, commision);
            trans.newBalance = account.Balance;
            ValidateNewBalance(account.Balance);
            _unitOfwork.SaveEntities(account, trans);
            return account;
        }

        public decimal evalNewBalance(decimal currentBalance, decimal amount, decimal commission)
        {
            decimal newBalance = currentBalance - (amount + commission);
            return newBalance;
        }

        public void ValidateNewBalance(decimal newBalance)
        {
            if(newBalance<0)
            {
                throw new Exception("new Balance cant be negative");
            }
        }
    }
}
