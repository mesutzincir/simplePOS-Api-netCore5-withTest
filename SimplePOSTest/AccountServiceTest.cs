using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NSubstitute;
using SimplePOS.Controllers;
using SimplePOS.DTOs;
using SimplePOS.Models;
using SimplePOS.Repositories;
using SimplePOS.Services;
using System;
using Xunit;

namespace SimplePOSTest
{
    public class AccountServiceTest
    {
        IAccountRepository mockAccountRepository;
        ITransactionRepository mockTransRepository;
        private IUnitOfWork _unitOfwork;
        ICommissionFactory mockCommisionFactory;
        IAccountService accountService;
        public AccountServiceTest()
        {
            mockAccountRepository = Substitute.For<IAccountRepository>();
            mockTransRepository = Substitute.For<ITransactionRepository>();
            _unitOfwork = Substitute.For<IUnitOfWork>();
            mockCommisionFactory = Substitute.For<ICommissionFactory>();
            accountService = new AccountService(mockAccountRepository, mockTransRepository, mockCommisionFactory, _unitOfwork);
        }
        [Fact]
        public void GetAccountById()
        {
            Account account = new Account(4755, 1001.88m);
            mockAccountRepository.GetById(account.AccountId).Returns(account);
            //act
            var result = accountService.GetAccountById(account.AccountId);

            //Assert
            mockAccountRepository.Received(1).GetById(account.AccountId);
            Assert.NotNull(result);
            Assert.Equal(account, result);

        }

        [Fact]
        public void make_payment()
        {
            Account account = new Account(4755, 1001.88m);
            Transaction transaction = new Transaction();
            transaction.MessageType = "PAYMENT";
            transaction.TransactionId = "fgsf-sgf-sfg-f-gsf-gs-g";
            transaction.AccountId = account.AccountId;
            transaction.Origin = "VISA";
            transaction.Amount = 300;
            //decimal endBalance = 10.56m;
            mockAccountRepository.GetById(account.AccountId).Returns(account);
            //mockAccountRepository.Save(Arg.Any<Account>()).Returns(account);
            //act
            var actual = accountService.SaveTransaction(transaction);

            //Assert
            mockAccountRepository.Received(1).GetById(account.AccountId);
            _unitOfwork.Received(1).SaveEntities(account,transaction);
            //mockTransRepository.Received(1).Save(transaction);
            Assert.True(actual >= 0);
            Assert.Equal(account.Balance, actual);
            Assert.Equal(transaction.newBalance, actual);
        }

        [Fact]
        public void newBalance_cant_be_negative()
        {
            decimal newBalance = -0.01m;
            Action act = () => accountService.ValidateNewBalance(newBalance);
            Exception ex = Assert.Throws<Exception>(act);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(0)]
        public void newBalance_should_greater_or_equels_zero(decimal newBalance)
        {
            accountService.ValidateNewBalance(newBalance);
        }
    }
}
