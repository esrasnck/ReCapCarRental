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
    public class RentalManager : IRentalService
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


        public IResult DeleteRentalCar(Rental rental) 
        {

            if (_rentalDal.Any(x => x.RentalId == rental.RentalId))
            {

                Rental rentalFind = _rentalDal.GetByID(rental.RentalId);
                if (rentalFind != null)
                { 
                    if (rentalFind.ReturnDate == null)
                    {
                        return new ErrorResult(Messages.RentalNotDeleted);
                    }
                    else
                    {
                        _rentalDal.Delete(rental);
                        return new SuccessResult(Messages.RentalDeleted);
                    }
                }
                return new ErrorResult(Messages.RentalNotDeleted);
            }
            return new ErrorResult(Messages.RentalNotDeleted);
        }

        public IResult UpdateRentalCar(Rental rental)
        {
            if (_rentalDal.Any(x => x.RentalId == rental.RentalId))
            {

                Rental rentalToUpdate = _rentalDal.GetByID(rental.RentalId);
                if (rentalToUpdate == null)
                {
                    return new ErrorResult(Messages.RentalNotUpdated);
                }
                else
                {
                    if (rental.CarId >0)
                    {
                        rentalToUpdate.CarId = rental.CarId;
                    }

                    if (rental.CustomerId >0)
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
            }
            return new ErrorResult(Messages.RentalNotUpdated);
        }

        public IResult AddRentalCar(Rental rental)
        {

            if (_rentalDal.Any(x=>x.CarId == rental.CarId))
            {
                if (_rentalDal.GetByID(rental.CarId).ReturnDate.HasValue)
                {
                    _rentalDal.Add(rental);
                    return new SuccessResult(Messages.RentalAdded);
                }

                return new ErrorResult(Messages.CarAlreadyRented);
            }
            else
            {
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.RentalAdded);
            }
        }
    }
}
