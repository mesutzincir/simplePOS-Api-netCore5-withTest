using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimplePOS.DTOs;
using SimplePOS.Models;
using SimplePOS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimplePOS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountService _accountService { get; set; }
        private readonly IMapper _mapper;


        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AccountDto> GetAccountById([Range(1000, 9999)]  long id)
        {            
            Account account = _accountService.GetAccountById(id);
            if(account== null)
            {
                return NotFound();
            }
            AccountDto dto = _mapper.Map<AccountDto>(account);
            return Ok(dto);
        }


        [HttpPost]
        [Route("save-transaction")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<decimal> SaveTransaction([FromBody][Required] TransactionDto transactionDto)
        {
            Transaction trans = _mapper.Map<Transaction>(transactionDto);
            decimal endBalance = _accountService.SaveTransaction(trans);
            return Ok(endBalance);
        }

    }
}
