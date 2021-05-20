using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.DTOs
{
    public class AccountDto
    {
        public long AccountId { get; set; }

        public decimal Balance { get; set; }
    }
}
