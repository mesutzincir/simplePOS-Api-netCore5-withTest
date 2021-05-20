
using SimplePOS.Services;
using System;
using Xunit;

namespace SimplePOSTest
{
    public class CommissionFactoryTest
    {        
        [Fact]
        public void create_visa_commission_service()
        {
            ICommissionFactory factory = new CommissionFactory();
            ICommissionService commissionService = factory.Create("VISA");
            Assert.IsType<VisaCommissionService>(commissionService);
        }

        [Fact]
        public void create_master_commission_service()
        {
            ICommissionFactory factory = new CommissionFactory();
            ICommissionService commissionService = factory.Create("MASTER");
            Assert.IsType<MasterCommissionService>(commissionService);
        }

        [Fact]
        public void exception_for_invalid_origin()
        {
            ICommissionFactory factory = new CommissionFactory();            
            Action act = () => factory.Create("INVALID");
            //assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            //Assert.Equal("Invalid origin type (Parameter 'origin')", exception.Message);

        }
    }
}
