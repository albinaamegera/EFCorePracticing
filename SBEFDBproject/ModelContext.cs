using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SBEFDBproject
{
    public class ModelContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SBEFCoreModuleDB;Trusted_Connection=True;");
        }

    }
    public class Customer
    {
        public int Id { get; set; }
        public string? SecondName { get; set; }
        public string? Name { get; set; }
        public string? Patronymic { get; set; }
        public string? Number { get; set; }
        public string? Email { get; set; }

    }
    public class Product
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
