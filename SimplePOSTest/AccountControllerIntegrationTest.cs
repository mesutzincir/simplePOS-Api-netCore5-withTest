using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NSubstitute;
using SimplePOS.Controllers;
using SimplePOS.DTOs;
using SimplePOS.Models;
using SimplePOS.Repositories;
using SimplePOS.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimplePOSTest
{
    public class AccountControllerIntegrationTest : IClassFixture<WebApplicationFactory<SimplePOS.Startup>>
    {
        private readonly HttpClient _client;
        

        public AccountControllerIntegrationTest(WebApplicationFactory<SimplePOS.Startup> fixture)
        {
            _client = fixture.CreateClient();
            SeedData();
        }

        internal void SeedData()
        {
            var options = new DbContextOptionsBuilder<SimplePOSDbContext>().UseInMemoryDatabase("posDb").Options;
            using (var context = new SimplePOSDbContext(options))
            {
                context.Database.EnsureCreated();
                if (context.Accounts.ToList().Count == 0)
                {
                    context.Accounts.Add(new Account(4755, 1001.88m));
                    context.Accounts.Add(new Account(9834, 456.45m));
                    context.Accounts.Add(new Account(7735, 89.36m));
                    int count = context.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task GetById_Should_Return_Account()
        {
            String url = "/api/Account/4755";
            var response = await _client.GetAsync(url);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var account = JsonConvert.DeserializeObject<Account>(await response.Content.ReadAsStringAsync());
            account.Should().NotBeNull();
            Assert.True(account.Balance >= 0);
        }

        [Fact]
        public async Task makePayment_Should_Return_Account()
        {
            var response = await MakePayment("sfdgs-sfg-sfg-sdf-gsdf-ff");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            String result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            var account = JsonConvert.DeserializeObject<decimal>(result);
            //account.Should().NotBeNull();
            Assert.True(account >= 0);
        }

        async Task<HttpResponseMessage> MakePayment(string transId)
        {
            HttpContent content = new StringContent(
               "{\"messageType\": \"PAYMENT\",\"transactionId\": \"" + transId + "\", \"accountId\": 4755, \"origin\": \"VISA\", \"amount\": 100}"
               , Encoding.UTF8, "application/json"
                );
            String url = "/api/Account/save-transaction";
            var response = await _client.PostAsync(url, content);
            return response;
        }

        [Fact]
        public async Task makeadjustment_Should_Return_Account()
        {
            string transId = "dgsdfgs-dfg-sdg-sf-g";
            //first make payment
            var response1 = await MakePayment(transId);
            response1.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpContent content = new StringContent(
               "{\"messageType\": \"ADJUSTMENT\",\"transactionId\": \"" + transId + "\", \"accountId\": 4755, \"origin\": \"VISA\", \"amount\": 50}"
               , Encoding.UTF8, "application/json"
                );
            String url = "/api/Account/save-transaction";
            var response = await _client.PostAsync(url, content);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            String result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            var account = JsonConvert.DeserializeObject<decimal>(result);
            //account.Should().NotBeNull();
            Assert.True(account >= 0);
        }
    }
}
