using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
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
        public IResult AddACar(Car car)
        {

            if (car.CarName.Length < 2)
            {
              return  new ErrorResult(Messages.CarNotAdded);
            }
            else if (car.DailyPrice <= 0)
            {

                // ekleme yapmalısın

                return new ErrorResult(Messages.CarNotAdded);
            }
            else
            {
                _carDal.Add(car);
         
                return new SuccessResult(Messages.CarAdded);
            }

        }

        public IResult UpdateCar(Car car)
        {
            Car carFind = _carDal.GetByID(car.CarId);
            if (carFind == null)
            {

                // ekleme yap

                return new ErrorResult(Messages.CarNotUpdated);
            }
            else
            {
                _carDal.Update(car);
               return new SuccessResult(Messages.CarAdded);
            }
         
        }

        public IResult DeleteCar(Car car)
        {
            Car carFind = _carDal.GetByID(car.CarId);
            if (carFind == null)
            {

              return  new ErrorResult(Messages.CarNotDeleted);
            }
            else
            {
                _carDal.Delete(car);
                return new SuccessResult(Messages.ColorDeleted);
            }

           
        }


        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour ==22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintanceTime);
            }
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(),Messages.CarListed);
        }

        public IDataResult<Car> GetByCarId(int id)
        {
            return new SuccessDataResult<Car> (_carDal.Get(x=> x.CarId == id),Messages.CarByID);
        }
        public IDataResult<List<Car>> GetByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.BrandId == id),Messages.CarByBrandID);
        }

        public IDataResult<List<Car>> GetByColorId(int id)
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(x => x.ColorId == id),Messages.CarByColorID);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>> (_carDal.GetCarDetail(),Messages.CarDetailList);
        }

       
    }
}
