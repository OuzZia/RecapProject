using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
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
    public class CarManager : ICarService
    {
        ICarDal _iCarDal;

        public CarManager(ICarDal iCarDal)
        {
            _iCarDal = iCarDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfDailyPriceValid(car.DailyPrice),CheckIfModelYearValid(car.ModelYear));
            if (result!=null)
            {
                return result;
            }
            _iCarDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Car car)
        {
            var result = _iCarDal.Get(c => c.Id == car.Id);
            if (result == null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _iCarDal.Delete(car);
            return new Result(true, Messages.Deleted);
        }
        public IDataResult<List<Car>> GetAll()
        {
            IResult result = BusinessRules.Run(CheckIfDayTimeValid());
            if (DateTime.Now.Hour == 21)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_iCarDal.GetAll(), Messages.CarsListed);
        }
        public IDataResult<List<Car>> GetAllByBrandId(int id)
        {
            var result = _iCarDal.GetAll(c => c.BrandId == id);
            if (result.Count < 1)
            {
                return new ErrorDataResult<List<Car>>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
        }
        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            var result = _iCarDal.GetAll(c => c.ColorId == id);
            if (result.Count < 1)
            {
                return new ErrorDataResult<List<Car>>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
        }
        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            var result = _iCarDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max);
            if (result.Count < 1)
            {
                return new ErrorDataResult<List<Car>>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<List<Car>>(result, Messages.CarsListed);
        }

        public IDataResult<Car> GetById(int id)
        {
            var result = _iCarDal.Get(c => c.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Car>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<Car>(result, Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_iCarDal.GetCarDetails(), Messages.CarsListed);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(CheckIfModelYearValid(car.ModelYear),CheckIfDailyPriceValid(car.DailyPrice));
            if (result!=null)
            {
                return result;
            }
            _iCarDal.Update(car);
            return new SuccessResult(Messages.Updated);

        }
        private IResult CheckIfDayTimeValid()
        {
            if (DateTime.Now.Hour==21)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            return new SuccessResult();
        }
        private IResult CheckIfDailyPriceValid(decimal dailyPrice)
        {
            if (dailyPrice<250||dailyPrice>800)
            {
                return new ErrorResult(Messages.InvalidDailyPrice);
            }
            return new SuccessResult();
        }
        private IResult CheckIfModelYearValid(int modelYear)
        {
            if (modelYear<2020)
            {
                return new ErrorResult(Messages.InvalidModelYear);
            }
            return new SuccessResult();
        }
    }
}
