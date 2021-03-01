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
using System.Linq;

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
         
            return new SuccessDataResult<List<CarImage>>(CheckCarImageExists(carId),
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
            FileUploadHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IResult ImageUpdate(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileUploadHelper.Update(_carImageDal.Get(x => x.CarId == carImage.CarId).ImagePath,file);
            carImage.Date =DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }

        //todo: refactor et !!!
        private IResult CheckImageCount(CarImage carImage)
        {
            List<CarImage> getCarImages = _carImageDal.GetAll(x => x.CarId == carImage.CarId);
            if (getCarImages.Count >= 5)
            {
                return new ErrorResult(Messages.OverCountCarImage);
            }

            return new SuccessResult();
        }
        
        private List<CarImage> CheckCarImageExists(int carId)
        {
            var result = _carImageDal.GetAll(x => x.CarId ==carId).Any();
            string path = @"\wwwroot\Images\mslogo.jpg";
            if (!result)
            {
                List<CarImage> carImages = new List<CarImage>()
                {
                   new CarImage{CarId = carId,ImagePath =path}
                };
                return carImages;
            }
            return _carImageDal.GetAll(x => x.CarId == carId);
        }
    }
}
