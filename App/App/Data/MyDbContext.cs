using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
           : base(options)
        {
        }

        public DbSet<AgroToxic> AgroToxic { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<Incentive> Incentive { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Company> Company { get; set; }
    }
}
