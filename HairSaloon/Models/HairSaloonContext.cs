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
        public DbSet<AddictService> AddictServices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Human> Humans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Rate> Rates { get; set; }
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
                    },
                    new Human
                    {
                        Id = 2,
                        FirstName = "Иван",
                        LastName = "Иванов",
                        PhoneNumber = "8777777777",
                        Login = "123",
                        Password = "123"
                    },
                    new Human
                    {
                        Id = 3,
                        FirstName = "Петр",
                        LastName = "Петров",
                        PhoneNumber = "866666666",
                        Login = "321",
                        Password = "321"
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
                   },
                   new Employee
                   {
                       Id = 2,
                       ManHaircut = true,
                       WomenHaircut = true,
                       Admin = false,
                       HumanId = 3
                   }
           );

            modelBuilder.Entity<AddictService>().HasData(
                   new AddictService
                   {
                       Id = 1,
                       Title = "Шампунь"
                   },
                   new AddictService
                   {
                       Id = 2,
                       Title = "Бальзам"
                   },
                   new AddictService
                   {
                       Id = 3,
                       Title = "Маски для волос"
                   },
                   new AddictService
                   {
                       Id = 4,
                       Title = "Краски для волос"
                   }
           );

            modelBuilder.Entity<Service>().HasData(
                  new Service
                  {
                      Id = 1,
                      Title = "Спортивная",
                      Price=300
                  },
                  new Service
                  {
                      Id = 2,
                      Title = "Под насадку",
                      Price = 200
                  },
                  new Service
                  {
                      Id = 3,
                      Title = "Каре",
                      Price = 800
                  },
                  new Service
                  {
                      Id = 4,
                      Title = "Укорачивание",
                      Price = 500
                  }
          );
        }
    }
}
