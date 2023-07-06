using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = RulesForAdding(rental);
            if (!result.Success)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.Rented);
        }

        public IResult Delete(Rental rental)
        {
            if (rental == null)
            {
                return new ErrorResult(Messages.NotDeleted);
            }
            _rentalDal.Delete(rental);
            return new Result(true, Messages.Deleted);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<List<Rental>> GetAllByCarId(int id)
        {
            var result = _rentalDal.GetAll(r => r.CarId == id);
            if (result.Count < 1)
            {
                return new ErrorDataResult<List<Rental>>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<List<Rental>>(result, Messages.RentalListed);
        }

        public IDataResult<List<Rental>> GetAllByCustomerId(int id)
        {
            var result = _rentalDal.GetAll(r => r.CustomerId == id);
            if (result.Count < 1)
            {
                return new ErrorDataResult<List<Rental>>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<List<Rental>>(result, Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            var result = _rentalDal.Get(r => r.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Rental>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<Rental>(result, Messages.RentalListed);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalListed);
        }

        public IResult Update(Rental rental)
        {
            if (rental == null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _rentalDal.Update(rental);
            return new Result(true, Messages.Deleted);
        }
        public IResult RulesForAdding(Rental rental)
        {
            IResult result = BusinessRules.Run(/*CheckIfReturnDateIsEarlierThanRentDate(rental.ReturnDate, rental.RentDate),*/CheckIfThisCarReturned(rental),CheckIfThisCarIsAlreadyRentedSelectedDateRange(rental));
            if (result!=null)
            {
                return result;
            }
            return new SuccessResult();
        }
        private IResult CheckIfThisCarReturned(Rental rental)
        {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result !=null)
            {
                if (rental.ReturnDate==null || rental.ReturnDate>result.RentDate)
                {
                    return new ErrorResult(Messages.ThisCarNotReturned);
                }
            }
            return new SuccessResult();
        }
        private IResult CheckIfThisCarIsAlreadyRentedSelectedDateRange(Rental rental)
        {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId
            && (r.RentDate.Date == rental.RentDate.Date
            || (r.RentDate.Date < rental.RentDate.Date
            && (r.ReturnDate == null
            || ((DateTime)r.ReturnDate).Date > rental.RentDate.Date))));
            if (result!=null)
            {
                return new ErrorResult(Messages.ThisCarCanNotBeRentedForThisRentDate);
            }
            return new SuccessResult();
        }
    }
}
