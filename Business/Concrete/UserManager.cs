using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        //[ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new Result(true, Messages.Added);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }
        public IResult Delete(User user)
        {
            if (user==null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _userDal.Delete(user);
            return new Result(true, Messages.Deleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(u => u.Id == id);
            if (result==null)
            {
                return new ErrorDataResult<User>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<User>(result,Messages.UsersListed);
        }

        public IResult Update(User user)
        {
            if (user==null||user.Id==0)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _userDal.Update(user);
            return new Result(true, Messages.Updated);
        }
    }
}
