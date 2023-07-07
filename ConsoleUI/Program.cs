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
            //CarManager carManager = new CarManager(new EfCarDal());
            //var result = carManager.GetCarDetails();
            //if (result.Success)
            //{
            //    foreach (var car in result.Data)
            //    {
            //        Console.WriteLine(car.Description + " " + car.BrandName + " " + car.ColorName + " " + result.Message);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

            //var result1 = carManager.GetAll();
            //if (result1.Success)
            //{
            //    foreach (var item in result1.Data)
            //    {
            //        Console.WriteLine(item.Description);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result1.Message);
            //}

            //var result = carManager.GetById(10);
            //if (result.Success)
            //{
            //    Console.WriteLine(result.Data.Description);
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}
            //UserManager userManager = new UserManager(new EfUserDal());
            //User user = new User();
            //user.FirstName = "Oguz";
            //user.LastName = "Ziya";
            //user.Email = "ouz.zia06@gmail.com";
            //user.Password = "12311231232";

            //var result =  userManager.Add(user);
            //if (result.Success)
            //{
            //    Console.WriteLine(result.Message);
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}
            //var result1 = userManager.GetById(3);
            //if (result1.Success)
            //{
            //    Console.WriteLine(result1.Data.FirstName);
            //}
            //else
            //{
            //    Console.WriteLine(result1.Message);
            //}

            //Car car1 = new Car();
            //car1.BrandId = 3;
            //car1.ColorId = 3;
            //car1.DailyPrice = 150;
            //car1.Description = "dizel man";
            //car1.ModelYear = 2021;
            //carManager.Add(car1);
            //var deletedCar = carManager.GetById();
            //carManager.Delete(deletedCar);

            //RentalManager rentalManager = new RentalManager(new EfRentalDal());

            //var result =rentalManager.GetRentalDetails();
            //if (result.Success)
            //{
            //    foreach (var rentalDetail in result.Data)
            //    {
            //        Console.WriteLine(rentalDetail.CompanyName + " " + rentalDetail.DailyPrice + " " + rentalDetail.Description + " " + rentalDetail.FirstName + " " + rentalDetail.LastName + " " + rentalDetail.RentDate + " " + rentalDetail.ReturnDate + " " + rentalDetail.Id);
            //    }
            //}

            //CarManager carManager = new CarManager(new EfCarDal());
            //var result = carManager.GetByDailyPrice(10,500);
            //if (result.Success)
            //{
            //    foreach (var item in result.Data)
            //    {
            //        Console.WriteLine(item.Description + " " + item.DailyPrice);
            //    }
            //}
            //Console.WriteLine(result.Message);

            CarImageManager carImageManager = new CarImageManager(new EfCarImageDal());
            var result = carImageManager.GetAll();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.CarId + " " + item.ImagePath + " "+ item.Id + " " + item.Date);
                }
            }
        }

    }
}
