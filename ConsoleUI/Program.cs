using Bogus.DataSets;
using Bussiness.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {


            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());

            //InputFromConsole(carManager);
            //CarList(carManager);

            // VeriEkleme();
           

            //IDataResult<List<CarDetailDto>> result = carManager.GetCarDetails();

            //if (result.Success ==true)
            //{
            //    foreach (CarDetailDto item in result.Data)
            //    {
            //        Console.WriteLine($"Araba Adı : {item.CarName}, Marka Adı :{item.BrandName}, Araba Rengi : {item.ColorName}, Günlük fiyatı :{item.DailyPrice}");
            //    }

            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}




        }

        private static void CarList(CarManager carManager)
        {
            foreach (Car item in carManager.GetAll().Data)
            {
                Console.WriteLine($"Araba Açıklaması : {item.CarName} - Fiyatı : {item.DailyPrice} - Yılı : {item.ModelYear}");
            }
        }

        private static void InputFromConsole(CarManager carManager)
        {
            Console.WriteLine("Araba yılı giriniz");
            string modelYear = Console.ReadLine();
            Console.WriteLine("araba ücreti giriniz");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Araba Adı giriniz");
            string aciklama = Console.ReadLine();

            Car car = new Car();


            car.ModelYear = modelYear;
            car.DailyPrice = price;
            car.CarName = aciklama;
            car.BrandId = 1;  // default olarak. şimdilik
            car.ColorId = 1; // default olarak. şimdilik
            carManager.AddACar(car);
        }

        private static void VeriEkleme()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());

            Random rnd = new Random();

            for (int i = 0; i < 5; i++)
            {
                Color color = new Color();
                color.ColorName = new Commerce("tr").Color();
                colorManager.AddAColor(color);

                Brand brand = new Brand();
                brand.BrandName = new Commerce("tr").Categories(1)[0];
                brandManager.AddBrand(brand);

                for (int j = 0; j < 5; j++)
                {
                    Car car = new Car();
                    car.BrandId = brand.BrandId;
                    car.ColorId = color.ColorId;
                    car.CarName = new Commerce("tr").ProductName();
                    car.DailyPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    car.Description = new Lorem("tr").Sentence(100);
                    car.ModelYear = rnd.Next(1999, 2021).ToString();
                    carManager.AddACar(car);
                }
            }
            Console.WriteLine("eklendi");
        }
    }
}
