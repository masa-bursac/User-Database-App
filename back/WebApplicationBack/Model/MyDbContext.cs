using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationBack.Model
{
    public class MyDbContext : DbContext
    {
       public DbSet<User> Users { get; set; }

       public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(1, "admin@gmail.com","admin", "Miki", "Milic", new DateTime(1998, 04, 20), UserType.admin)
            );
        }

    }

}
