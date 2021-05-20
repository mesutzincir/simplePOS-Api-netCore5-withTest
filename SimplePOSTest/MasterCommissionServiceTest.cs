using SimplePOS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimplePOSTest
{
    public class MasterCommissionServiceTest
    {
        [Theory]
        [InlineData(1000, 20)]
        [InlineData(0, 0)]
        [InlineData(0.1, 0)]
        [InlineData(0.26, 0.01)]
        [InlineData(0.25, 0)]
        public void eval_master_commission(decimal amount, decimal expected)
        {
            ICommissionService visaService = new MasterCommissionService();
            decimal actual = visaService.Evaluate(amount);
            Assert.Equal(expected, actual);

        }
    }
}
