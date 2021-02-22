using Bussiness.Abstract;
using Bussiness.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Dtos;

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
        public IResult AddRentalCar(RentalAddDto rental) // buraya da
        {
            foreach ( RentalAddDto  item in _rentalDal.RentalAdd())
            {
                if (item.CarId == rental.CarId && item.ReturnDate !=null)
                {
                    Rental rent = new Rental();
                    rent.CarId = rental.CarId;
                    rent.CustomerId = rental.CustomerId;
                    _rentalDal.Add(rent);
                    return new SuccessResult(Messages.RentalAdded);
                }
                return new ErrorResult(Messages.RentalNotAdded);
            }
            return new ErrorResult(Messages.RentalNotAdded);
   
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
