using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Concrete
{
    public class RentalManager:IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetByID(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.RentalId == id), Messages.RentalById);
        }
        public IResult AddRentalCar(Rental rental) // buraya da
        {
            Rental carForRent = _rentalDal.GetByID(rental.RentalId);
            if (carForRent.ReturnDate == null && carForRent.RentDate!=null)
            {
                return new ErrorResult(Messages.RentalNotAdded);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);


        }

        public IResult DeleteRentalCar(Rental rental) // buraya daha beter dalacaz
        {
            Rental rentalFind = _rentalDal.GetByID(rental.RentalId);
            if (rentalFind == null)
            {
                return new ErrorResult(Messages.RentalNotDeleted);
            }
            else
            {
                _rentalDal.Delete(rental);
                return new SuccessResult(Messages.RentalDeleted);
            }
        }

        public IResult UpdateRentalCar(Rental rental)
        {
            Rental rentalFind = _rentalDal.GetByID(rental.RentalId);
            if (rentalFind == null)
            {
                return new ErrorResult(Messages.RentalNotUpdated);
            }
            else
            {
                _rentalDal.Update(rental);
                return new SuccessResult(Messages.RentalUpdated);
            }
        }
    }
}
