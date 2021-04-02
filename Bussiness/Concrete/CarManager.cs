using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Bussiness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using FluentValidation;
using Bussiness.Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Business;

namespace Bussiness.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private IBrandService _brandService;
        private IColorService _colorService;


        public CarManager(ICarDal carDal,IBrandService brandService, IColorService colorService)
        {
            _carDal = carDal;
            _brandService = brandService;
            _colorService = colorService;
        }

        [SecuredOperation("admin")]

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult AddACar(Car car)
        {
            var result = BusinessRules.Run(CheckIfCarNameExists(car.CarName), IsColorExists(car.ColorId), IsBrandExists(car.BrandId));
            if (result !=null)
            {
                return new ErrorResult(Messages.CarNotAdded);
            }

            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }
      
        
        [CacheRemoveAspect("ICarService.Get")]
        public IResult UpdateCar(Car car)
        {
            var result = BusinessRules.Run(IsCarExists(car.CarId));
            if (result !=null)
            {
                return new ErrorResult(Messages.CarNotUpdated);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);

        }


        [CacheRemoveAspect("ICarService.Get")]
        public IResult DeleteCar(Car car)
        {
            var result = BusinessRules.Run(IsCarExists(car.CarId));
            if (result!=null)
            {
                return new ErrorResult(Messages.CarNotDeleted);
            }
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);

        }

      //  [SecuredOperation("admin")]

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        [CacheAspect]
        public IDataResult<Car> GetByCarId(int id)
        {
            var result = BusinessRules.Run(IsCarExists(id));
            if (result!=null)
            {
                return new ErrorDataResult<Car>(Messages.NotCarByID);
            }

            return new SuccessDataResult<Car>(_carDal.Get(x => x.CarId == id), Messages.CarByID);
        }

       
        public IDataResult<List<Car>> GetByBrandId(int id)
        {

            var result = BusinessRules.Run(IsBrandExists(id));
            if (result!=null)
            {
                return new ErrorDataResult<List<Car>>(Messages.NotCarByColorID);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.BrandId == id), Messages.CarByBrandID);

        }
      
        public IDataResult<List<Car>> GetByColorId(int id)
        {
            var result = BusinessRules.Run(IsColorExists(id));
            if (result!=null)
            {
                return new ErrorDataResult<List<Car>>(Messages.NotCarByColorID);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.ColorId == id), Messages.CarByColorID);
        }


        // dto mevzusu
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
          
            if (DateTime.Now.Hour == 7)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(), Messages.CarDetailList);
        }


        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            var result = BusinessRules.Run(IsColorExists(colorId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NotCarByColorID);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(x => x.ColorId == colorId), Messages.CarDetailList);
        }


        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            var result = BusinessRules.Run(IsBrandExists(brandId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NotCarByColorID);
            }

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(x => x.BrandId == brandId), Messages.CarDetailList);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId)
        {
            var result = BusinessRules.Run(IsCarExists(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.NotCarByID);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(x => x.CarId == carId), Messages.CarDetailList);
        }


        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId)
        {
            var result = BusinessRules.Run(IsColorExists(colorId), IsBrandExists(brandId));
            if (result!=null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.CarNotListed);
            }


            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(c => c.ColorId == colorId && c.BrandId == brandId),Messages.CarByBrandAndCar);
        }



        private IResult  IsBrandExists(int brandId)
        {
            var result = _brandService.GetByBrandId(brandId);
            if (result!=null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult IsColorExists(int colorId)
        {
            var result = _colorService.GetByColorID(colorId);
            if (result!=null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult IsCarExists(int carId)
        {
            var result = _carDal.GetByID(carId);
            if (result!=null)   // ==null error gönder
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult CheckIfCarNameExists(string carName)
        {
            if (!String.IsNullOrEmpty(carName))
            {
                var result = _carDal.Any(x => x.CarName.ToLower().Contains(carName.ToLower()));
                if (!result)
                {
                    return new SuccessResult();
                }
            }
            return new ErrorResult();
        }
    }
}
