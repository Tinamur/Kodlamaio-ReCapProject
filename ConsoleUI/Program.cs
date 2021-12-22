using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Car CRUD DENENDİ İŞE YARIYOR
            //Brand CRUD DENENDİ İŞE YARIYOR
            //Color CRUD DENENDİ İŞE YARIYOR
            // CarDTO işe yarıyor 
            IRentalService rentalService = new RentalManager(new EfRentalDal());
            rentalService.Add(new Rental
            {
                CarId = 4,
                CustomerId = 1,
                RentDate = DateTime.Now,
            });


        }

        static void WriteCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.WriteLine
                    (car.CarName + " - " + car.DailyPrice + " - " + car.Description);
            }
        }
    }
}
