using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Dtos;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;

namespace Bussiness.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICarService carService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetByID(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.RentalId == id), Messages.RentalById);
        }


        // [CacheRemoveAspect("IRentalService.Get")]
        public IResult DeleteRentalCar(Rental rental)
        {

            var result = BusinessRules.Run(CheckIsRentalExits(rental.RentalId));

            if (result != null)
            {
                return new ErrorResult(Messages.RentalNotDeleted);
            }
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);

        }

        // [CacheRemoveAspect("IRentalService.Get")]
        public IResult UpdateRentalCar(Rental rental)
        {
            var result = BusinessRules.Run(CheckIsRentalExits(rental.RentalId));
            if (result != null)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }

            Rental rentalToUpdate = _rentalDal.GetByID(rental.RentalId); // todo: buraya bişiler düşüncez

            if (rental.CarId > 0)
            {
                rentalToUpdate.CarId = rental.CarId;
            }

            if (rental.CustomerId > 0)
            {
                rentalToUpdate.CustomerId = rental.CustomerId;
            }

            if (rental.ReturnDate != null)
            {
                rentalToUpdate.ReturnDate = rental.ReturnDate;
            }
            _rentalDal.Update(rentalToUpdate);
            return new SuccessResult(Messages.RentalUpdated);

        }
        // [CacheRemoveAspect("IRentalService.Get")]
        public IResult AddRentalCar(Rental rental)
        {

            var result = BusinessRules.Run(CheckIfCarIdExist(rental.CarId), IsCarAvaliable(rental.CarId));
            if (result != null)
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);

        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsDto()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalDetailListed);
        }

        //public IDataResult<Rental> GetByCarId(int carId)
        //{
           

        //}
        private IResult CheckIfCarIdExist(int carId)
        {
            var result = _carService.GetByCarId(carId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult IsCarAvaliable(int carId)
        {
            var result = _rentalDal.FirstOrDefault(x => x.CarId == carId);
            if (result != null)
            {
                if (result.ReturnDate.HasValue && result.ReturnDate <= DateTime.Now)
                {
                    return new SuccessResult();
                }
                return new ErrorResult();
            }
            return new SuccessResult();

        }

        private IResult CheckIsRentalExits(int rentalId)
        {
            var result = _rentalDal.Any(x => x.RentalId == rentalId);
            if (!result)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
