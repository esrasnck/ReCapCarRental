using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Abstract
{
   public interface ICarService
    {
        /// <summary>
        /// Arabaları listeler.
        /// </summary>
        /// <returns>Araba listesi döndürür.</returns>
        List<Car> GetAll();
        Car GetByCarId(int id);
        void AddACar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(Car car);
        List<Car> GetByColorId(int id);
        List<Car> GetByBrandId(int id);
        List<CarDetailDto> GetCarDetails();
    }
}
