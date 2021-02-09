using Bussiness.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class CarManager:ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        public void AddACar(Car car)
        {


            if (car.CarName.Length < 2)
            {
                Console.WriteLine("Araba açıklaması minimum 2 karakter olmalıdır.");
            }
            else if (car.DailyPrice <= 0)
            {
                Console.WriteLine(" ve Ürün fiyatı 0'dan büyük olmalıdır.");
            }
            else
            {
                _carDal.Add(car);
                Console.WriteLine("Eklenme başarılı");
            }




        }

        public void UpdateCar(Car car)
        {
            _carDal.Update(car);
        }

        public void DeleteCar(Car car)
        {
            _carDal.Delete(car);
        }
        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car GetByCarId(int id)
        {
            return _carDal.GetByID(id);
        }
        public List<Car> GetByBrandId(int id)
        {
            return _carDal.GetAll(x => x.BrandId == id);
        }

        public List<Car> GetByColorId(int id)
        {
            return _carDal.GetAll(x => x.ColorId == id);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            return _carDal.GetCarDetail();
        }
    }
}
