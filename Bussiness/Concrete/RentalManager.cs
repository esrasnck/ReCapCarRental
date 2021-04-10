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
using Core.Aspects.Autofac.Validation;
using Bussiness.ValidationRules.FluentValidation;

namespace Bussiness.Concrete
{
    public class RentalManager : IRentalService   // refactoring done
    {
        private IRentalDal _rentalDal;
        private ICarService _carService;
        private ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal, ICarService carService,ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
        }
        [CacheAspect(duration:60)]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }


        [CacheAspect(duration: 60)]
        public IDataResult<Rental> GetByID(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.RentalId == id), Messages.RentalById);
        }


        [CacheRemoveAspect("IRentalService.Get")]
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

        [CacheRemoveAspect("IRentalService.Get")]

        [ValidationAspect(typeof(RentalValidator))]
        public IResult UpdateRentalCar(Rental rental)
        {
            var result = BusinessRules.Run(CheckIsRentalExits(rental.RentalId));
            if (result != null)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }

            var result2 = BusinessRules.Run(CheckIfCarIdExist(rental.CarId));
            if (result2!=null)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }

           
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);

        }

       // [CacheRemoveAspect("IRentalService.Get")]
        public IResult AddRentalCar(Rental rental)
        {

            var result = BusinessRules.Run(CheckIfCarIdExist(rental.CarId), IsCarAvaliable(rental.CarId),FindeksCheck(rental.CarId,rental.CustomerId));
            if (result != null)
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);

        }

        [CacheAspect(duration: 60)]
        public IDataResult<List<RentalDetailDto>> GetRentalDetailsDto()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalDetailListed);
        }


 
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
            var result = _rentalDal.Any(x => x.CarId == carId && (x.ReturnDate == null || x.ReturnDate <= DateTime.Now));
            if (result)
            {

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

        private IResult FindeksCheck(int carId, int customerId)
        {
            var customerFindex = _customerService.Findeks(customerId);
            var carFindex = _carService.CarFindex(carId);
            if(carFindex.Data> customerFindex.Data)
            {
                return new ErrorResult(Messages.CannotRent);
            }
            return new SuccessResult(Messages.CanRent);
        }
    }
}
