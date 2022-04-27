using Core.Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //Context : Db tabloları ile Proje classlarını bağlamak
    //için kullanılır
    public class CarRentalContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //SQL server Olarak Nereye bağlanacak
            optionsBuilder.UseSqlServer
                (@"Server =(localdb)\MSSQLLocalDB;Database=CarRental;Trusted_Connection=True");
        }

        //Hangi Kategori Nereye Bağlanacak?
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<CarImage> CarImages { get; set; }


    }
}
