using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager:IColorService
    {
        IColorDal _iColorDal;

        public ColorManager(IColorDal iColorDal)
        {
            _iColorDal = iColorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            _iColorDal.Add(color);
            return new Result(true, Messages.Added);
        }
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Delete(Color color)
        {
            _iColorDal.Delete(color);
            return new Result(true, Messages.Deleted);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_iColorDal.GetAll(),Messages.ColorsListed);
        }
        public IDataResult<Color> GetById(int id)
        {
            var result = _iColorDal.Get(c => c.Id == id);
            if (result==null)
            {
                return new ErrorDataResult<Color>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<Color>(result,Messages.ColorsListed);
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            _iColorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }
    }
}
