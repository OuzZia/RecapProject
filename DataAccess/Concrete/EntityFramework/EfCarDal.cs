using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalDb>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarRentalDb context = new CarRentalDb())
            {
                var result = from c in context.Cars
                             join r in context.Colors
                             on c.ColorId equals r.Id
                             join b in context.Brands
                             on c.BrandId equals b.Id
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 ColorName = r.ColorName,
                                 BrandName = b.BrandName,
                                 BrandId = c.BrandId,
                                 ColorId = c.ColorId
                             };
                return result.ToList();
            }
        }
    }
}
