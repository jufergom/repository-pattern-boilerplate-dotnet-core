using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSq();
        }
        */

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
