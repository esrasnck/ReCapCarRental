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

namespace Bussiness.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("admin")]

        [ValidationAspect(typeof(CarValidator))]
        public IResult AddACar(Car car)
        {
            if (car != null)
            {

                if (!_carDal.Any(x=>x.CarName.Contains(car.CarName)))
                {
                    _carDal.Add(car);

                    return new SuccessResult(Messages.CarAdded);
                }
                else
                {
                    return new ErrorResult(Messages.CarNotAdded);
                }
            }
            return new ErrorResult(Messages.CarNotAdded);
        }

      
        public IResult UpdateCar(Car car)
        {
            if (_carDal.Any(x => x.CarId == car.CarId))
            {

                Car carFind = _carDal.GetByID(car.CarId);
                if (carFind != null)
                {
                    if (car.CarName != null && _carDal.Any(x => x.CarName != car.CarName))
                    {
                        carFind.CarName = car.CarName;
                    }
                    if (car.BrandId > 0 && _carDal.Any(x => x.BrandId != car.BrandId))
                    {
                        carFind.BrandId = car.BrandId;
                    }
                    if (car.ColorId > 0 && _carDal.Any(x => x.ColorId != car.ColorId))
                    {
                        carFind.ColorId = car.ColorId;
                    }
                    if (car.DailyPrice > 0 && _carDal.Any(x => x.DailyPrice != car.DailyPrice))
                    {
                        carFind.DailyPrice = car.DailyPrice;
                    }
                    if (car.Description != null && _carDal.Any(x => x.Description != car.Description))
                    {
                        carFind.Description = car.Description;
                    }
                    if (car.ModelYear != null && _carDal.Any(x => x.ModelYear != car.ModelYear))
                    {
                        carFind.ModelYear = car.ModelYear;
                    }

                    _carDal.Update(carFind);
                    return new SuccessResult(Messages.CarUpdated);
                }

                return new ErrorResult(Messages.CarNotUpdated);

            }

            return new ErrorResult(Messages.CarCantFind);
        }



        public IResult DeleteCar(Car car)
        {
            if (_carDal.Any(x => x.CarId == car.CarId))
            {
                Car carFind = _carDal.GetByID(car.CarId);

                List<CarForDeleteDto> carForDeleteDtos = _carDal.GetCars();
                foreach (var item in carForDeleteDtos)
                {
                    if (item.CarId == carFind.CarId && item.ReturnedDate != null)
                    {
                        _carDal.Delete(carFind);
                        return new SuccessResult(Messages.CarDeleted);

                    }
                }
                 return new ErrorResult(Messages.CarNotDeleted);

            }
            return new ErrorResult(Messages.CarNotDeleted);
        }


        public IDataResult<List<Car>> GetAll()
        {
            //if (DateTime.Now.Hour ==22)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.MaintanceTime);
            //}
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        public IDataResult<Car> GetByCarId(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(x => x.CarId == id), Messages.CarByID);
        }

       
        public IDataResult<List<Car>> GetByBrandId(int id)
        {
            if (id != 0 && _carDal.Any(x => x.BrandId == id))
            {
                List<Car> car = _carDal.GetAll(x => x.BrandId == id);
                if (car.Count != 0)
                {
                    return new SuccessDataResult<List<Car>>(car, Messages.CarByBrandID);
                }
                return new ErrorDataResult<List<Car>>(Messages.CarNotListed);
            }
            return new ErrorDataResult<List<Car>>(Messages.IdNotFound);




        }
      
        public IDataResult<List<Car>> GetByColorId(int id)
        {
            if (id != 0 && _carDal.Any(x => x.ColorId == id))
            {
                List<Car> car = _carDal.GetAll(x => x.ColorId == id);
                if (car.Count != 0)
                {
                    return new SuccessDataResult<List<Car>>(car, Messages.CarByColorID);
                }
                return new ErrorDataResult<List<Car>>(Messages.CarNotListed);
            }
            return new ErrorDataResult<List<Car>>(Messages.IdNotFound);

        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 21)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetail(), Messages.CarDetailList);
        }

    }
}
