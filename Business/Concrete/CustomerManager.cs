using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new Result(true, Messages.Added);
        }

        public IResult Delete(Customer customer)
        {
            var result = _customerDal.Get(c => c.Id == customer.Id);
            if (result == null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _customerDal.Delete(customer);
            return new Result(true, Messages.Deleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.CustomersListed);
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(c => c.Id == id);
            if (result==null)
            {
                return new ErrorDataResult<Customer>(Messages.InvalidEntity);
            }
            return new SuccessDataResult<Customer>(result, Messages.CustomersListed);
        }

        public IResult Update(Customer customer)
        {
            var result = _customerDal.Get(c => c.Id == customer.Id);
            if (result==null)
            {
                return new ErrorResult(Messages.InvalidEntity);
            }
            _customerDal.Update(customer);
            return new Result(true, Messages.Updated);
        }
    }
}
