using Microsoft.EntityFrameworkCore;
using SimplePOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimplePOS.Repositories
{
    public class SimplePOSDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SimplePOSDbContext(DbContextOptions<SimplePOSDbContext> options) : base(options)
        {

        }
        //to test
        public SimplePOSDbContext() {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Todo test purpose
            modelBuilder.Entity<Account>().HasData(
                new Account(4755, 1001.88m),
                new Account(9834, 456.45m),
                new Account(7735, 89.36m)
            );
           
        }
    }
}
