using SimplePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Services
{
    public interface ICommissionFactory
    {
        ICommissionService Create(string origin);
    }
    public class CommissionFactory : ICommissionFactory
    {
        public ICommissionService Create(string origin)
        {
            if(Transaction.VISA.Equals(origin))
            {
                return new VisaCommissionService();
            }
            else if(Transaction.MASTER.Equals(origin))
            {
                return new MasterCommissionService();
            }
            else
            {
                throw new ArgumentException("Invalid origin type", nameof(origin));
            }
        }
    }
}
