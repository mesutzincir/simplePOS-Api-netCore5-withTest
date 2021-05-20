using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Services
{
    public class MasterCommissionService : ICommissionService
    {
        const decimal RATE = 0.02m;
        public decimal Evaluate(decimal amount)
        {
            decimal com = amount * RATE;
            return Math.Round(com, 2);
        }
    }
}
