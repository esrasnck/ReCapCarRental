using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Abstract
{
    public interface ICarImageService
    {
        IResult ImageAdd(IFormFile file, CarImage carImage);

        IResult ImageUpdate(IFormFile file, CarImage carImage);

        IResult ImageDelete(CarImage carImage);

        IDataResult<List<CarImage>> GetList();

        IDataResult<List<CarImage>> GetByCarId(int carId);

        IDataResult<CarImage> GetImageById(int carImageId);
    }
}
