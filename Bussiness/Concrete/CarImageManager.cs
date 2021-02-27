using Bussiness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Bussiness.Constants.Messages;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        // todo: iş kuralları eklenecek !!!!



        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(x => x.CarId == carId),
                Messages.ListedByCarId);
        }

        public IDataResult<CarImage> GetImageById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.GetByID(carImageId),Messages.CarImage);
        }

        public IDataResult<List<CarImage>> GetList()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages.ImageListed);
        }

        public IResult ImageAdd(IFormFile file,CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageCount(carImage));
            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileUploadHelper.Add(file);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImageAdded);
        }

     

        public IResult ImageDelete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IResult ImageUpdate(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }

        //todo: refactor et !!!
        private IResult CheckImageCount(CarImage carImage)
        {
            List<CarImage> getCarImages = _carImageDal.GetAll(x => x.CarId == carImage.CarId);
            if (getCarImages.Count >= 5)
            {
                return new ErrorResult("bir aracın en fazla 5 resmi olabilir");
            }

            return new SuccessResult();
        }
    }
}
