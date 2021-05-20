using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SimplePOS.Controllers;
using SimplePOS.DTOs;
using SimplePOS.Models;
using SimplePOS.Services;
using System;
using Xunit;

namespace SimplePOSTest
{
    public class AccountControllerTest
    {
        private IAccountService mockAccountService;
        private IMapper autoMapper;
        private AccountController accountController;

        public AccountControllerTest()
        {
             mockAccountService = Substitute.For<IAccountService>();
             autoMapper = Substitute.For<IMapper>();
            accountController = new AccountController(mockAccountService, autoMapper);
        }


        [Fact]
        public void call_service_with_id()
        {
            Account account = new Account(7735, 1000m);
            AccountDto accountDto = new AccountDto { AccountId = 7735, Balance = 1000m };
            mockAccountService.GetAccountById(account.AccountId).Returns(account);
            autoMapper.Map<AccountDto>(account).Returns(accountDto);
            //act
            var actionResult = accountController.GetAccountById(account.AccountId);

            //Assert
            mockAccountService.Received(1).GetAccountById(account.AccountId);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as AccountDto;
            Assert.Equal(accountDto, actual);

        }

        [Fact]
        public void make_payment_call_service_one_time()
        {           
            TransactionDto paymentDto = new TransactionDto();
            paymentDto.MessageType = "PAYMENT";
            paymentDto.TransactionId = "fgsf-sgf-sfg-f-gsf-gs-g";
            paymentDto.AccountId = 9834;
            paymentDto.Origin = "VISA";
            paymentDto.Amount = 300;
            decimal endBalance = 10.56m;


            Transaction payment = new Transaction();
            payment.MessageType = "PAYMENT";
            payment.TransactionId = "fgsf-sgf-sfg-f-gsf-gs-g";
            payment.AccountId = 9834;
            payment.Origin = "VISA";
            payment.Amount = 300;

            autoMapper.Map<Transaction>(paymentDto).Returns(payment);
            mockAccountService.SaveTransaction(payment).Returns(endBalance);
            //act
            var actionResult = accountController.SaveTransaction(paymentDto);

            //Assert
            mockAccountService.Received(1).SaveTransaction(payment);
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result.Value);
            decimal actual = (decimal)result.Value;
            Assert.Equal(endBalance, actual);

        }
    }
}
