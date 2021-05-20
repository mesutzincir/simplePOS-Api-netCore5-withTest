using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplePOS.Models;
using SimplePOS.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedData();
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void SeedData()
        {
            var options = new DbContextOptionsBuilder<SimplePOSDbContext>().UseInMemoryDatabase("posDb").Options;
            using (var context = new Repositories.SimplePOSDbContext(options))
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
    }
}
