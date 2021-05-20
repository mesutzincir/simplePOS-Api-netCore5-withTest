using SimplePOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
namespace SimplePOSTest
{
    public class VisaCommissionServiceTest
    {
        [Theory]
        [InlineData(1000,10)]
        [InlineData(0, 0)]
        [InlineData(0.1, 0)]
        [InlineData(0.5, 0)]
        [InlineData(0.51, 0.01)]
        public void eval_visa_commission(decimal amount, decimal expected)
        {
            ICommissionService visaService = new VisaCommissionService();           
            decimal actual = visaService.Evaluate(amount);
            Assert.Equal(expected, actual);
            
        }
    }
}
