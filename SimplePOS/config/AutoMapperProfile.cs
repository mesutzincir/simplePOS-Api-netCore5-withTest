using AutoMapper;
using SimplePOS.DTOs;
using SimplePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.config
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
