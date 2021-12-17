using Business.Abstract;
using Business.Concrete;
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
            ICarService carManager = new CarManager(new InMemoryCarDal());
            Console.WriteLine("Default Cars Table");
            //Write Car Diye birşey yazdım aşağıda
            //Tek görevi yazdırmak
            WriteCars(carManager.GetAll());
            Console.WriteLine("----------------");

            Console.WriteLine("After We Add New Car with Desc_5");
            carManager.Add(new Car
            {
                CarId = 5,
                BrandId = 2,
                ColorId = 3,
                ModelYear=2008,
                DailyPrice=150,
                Description="Desc_5"
            });
            WriteCars(carManager.GetAll());
            Console.WriteLine("----------------");

            Console.WriteLine("After We Delete Car with id =3 (Desc_3)");
            //Böylelikle GetById yi de test etmiş Oluyoruz
            Car carWeWant = carManager.GetById(3);
            carManager.Delete(carWeWant);
            WriteCars(carManager.GetAll());
            Console.WriteLine("----------------");

            Console.WriteLine("After We Update Car with id =2");
            carWeWant = carManager.GetById(2);
            carWeWant.Description = "!!!This Car is Updated!!! ";
            carManager.Update(carWeWant);
            WriteCars(carManager.GetAll());
            Console.WriteLine("----------------");

        }

        static void WriteCars(List<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.WriteLine(car.Description);
            }
        }
    }
}
