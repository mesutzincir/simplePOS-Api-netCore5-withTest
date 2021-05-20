using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Models
{
    
    public class Account
    {        
        public long AccountId { get; set; }

        public decimal Balance { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public Account()
        {           
            Balance = 0;
            CreateDate = DateTime.Now;
        }
        public Account(long accountId, decimal balance = 0)
        {
            AccountId = accountId;
            Balance = balance;
            CreateDate = DateTime.Now;
        }


    }
}
