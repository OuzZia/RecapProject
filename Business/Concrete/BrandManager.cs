using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _iBrandDal;

        public BrandManager(IBrandDal iBrandDal)
        {
            _iBrandDal = iBrandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            _iBrandDal.Add(brand);
            return new Result(true, Messages.Added);
        }

        public IResult Delete(Brand brand)
        {
            var result = _iBrandDal.Get(b => b.Id == brand.Id);
            if (result==null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _iBrandDal.Delete(brand);
            return new Result(true, Messages.Deleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>> (_iBrandDal.GetAll(),Messages.BrandsListed);
        }

        public IDataResult<List<Brand>> GetAllByColorId(int id)
        {
            var result = _iBrandDal.GetAll(b => b.Id == id);
            if (result.Count<1)
            {
                return new ErrorDataResult<List<Brand>>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<List<Brand>> (result,Messages.BrandsListed);
        }

        public IDataResult<Brand> GetById(int id)
        {
            var result = _iBrandDal.Get(b => b.Id == id);
            if (result==null)
            {
                return new ErrorDataResult<Brand>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<Brand>(result, Messages.BrandsListed);
        }

        public IResult Update(Brand brand)
        {
            var result = _iBrandDal.Get(b => b.Id == brand.Id);
            if (result==null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _iBrandDal.Update(brand);
            return new Result(true, Messages.Updated);
        }
    }
}
