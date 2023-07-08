using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }


        //[ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile image, CarImage carImage)
        {
            IResult ruleResult = BusinessRules.Run(CheckImageLimitExceded(carImage.CarId));
            if (ruleResult!=null)
            {
                return ruleResult;
            }
            var imageResult = FileHelper.Add(image);
            carImage.ImagePath = imageResult.Message;
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        //[ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage image)
        {
            var carToBeDeleted = _carImageDal.Get(i => i.Id == image.Id);
            if (carToBeDeleted==null)
            {
                return new ErrorResult(Messages.CarImagesNotFound);
            }
            FileHelper.Delete(carToBeDeleted.ImagePath);
            _carImageDal.Delete(carToBeDeleted);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages.CarImagesListed);
        }

        public IDataResult<List<CarImage>> GetAllCarImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);
            if (result.Count==0)
            {
                result.Add(new CarImage
                {
                    CarId = carId,
                    ImagePath = "/Images/default.png"
                });
            }
            return new SuccessDataResult<List<CarImage>>(result);
        }
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }

        public IResult Update(IFormFile image, CarImage carImage)
        {
            var carToBeUpdated = _carImageDal.Get(i => i.Id == carImage.Id);
            if (carToBeUpdated == null)
            {
                return new ErrorResult(Messages.CarImagesNotFound);
            }
            var imageResult = FileHelper.Update(image, carToBeUpdated.ImagePath);
            carImage.ImagePath = imageResult.Message;
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
        public IDataResult <List<CarImage>> GetAllByCarId(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId);
            if (result.Count<1)
            {
                return new ErrorDataResult<List<CarImage>>(Messages.CarImagesNotFound);
            }
            return new SuccessDataResult<List<CarImage>>(result,Messages.CarImagesListed);
        }
        private IResult CheckImageLimitExceded(int carId)
        {
            var carImagesOfTheCar = _carImageDal.GetAll(i => i.CarId == carId);
            if (carImagesOfTheCar.Count >=5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }
            return new SuccessResult();
        }
        private IDataResult <List<CarImage>> GetDefaultImage(int carId)
        {
            List<CarImage> carImage = new List<CarImage>();
            carImage.Add(new CarImage { CarId = carId, Date = DateTime.Now, ImagePath = "DefaultImage.png" });
            return new SuccessDataResult<List<CarImage>>(carImage);
        }
        private IResult CheckImagePathIsNull(CarImage carImage)
        {
            if (carImage.ImagePath!=null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }


    }
}
