using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Services
{
    public interface ICommissionService
    {
        decimal Evaluate(Decimal amount);
    }
}
