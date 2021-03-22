using Core.Utilities.Results;
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
        IDataResult<List<Car>> GetAll();
        IDataResult<Car>GetByCarId(int id);
        IResult AddACar(Car car);
        IResult UpdateCar(Car car);
        IResult DeleteCar(Car car);

        IDataResult<List<CarDetailDto>> GetCarDetails();

        // todo : HANGİ TARAF DOĞRU BAK
        IDataResult<List<Car>> GetByColorId(int id);
        IDataResult<List<Car>> GetByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId);
        IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId);

        IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId);
        //IDataResult<List<CarDetailDto>> GetCarsByBrandId(int Brandid);
        //IDataResult<List<CarDetailDto>> GetCarsByColorId(int Colorid);
    }
}
