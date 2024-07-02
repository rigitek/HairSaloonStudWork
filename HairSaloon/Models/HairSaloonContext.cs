using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HairSaloon.Models
{
    class HairSaloonContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }

        public HairSaloonContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=HairSaloon.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Human>().HasData(
                    new Human
                    {
                        Id = 1,
                        FirstName = "Админ",
                        LastName = "Админ",
                        PhoneNumber = "89999999999",
                        Login = "admin",
                        Password = "admin"
                    }
            );

            modelBuilder.Entity<Employee>().HasData(
                   new Employee
                   {
                       Id = 1,
                       ManHaircut = false,
                       WomenHaircut = false,
                       Admin = true,
                       HumanId = 1
                   }
           );
        }
    }
}
