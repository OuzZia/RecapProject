using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1, BrandId=2, ColorId=4, DailyPrice=350, Description="13000Km Dizel", ModelYear=2022},
                new Car{Id=2, BrandId=1, ColorId=3, DailyPrice=550, Description="5000Km Dizel SUV", ModelYear=2022},
                new Car{Id=3, BrandId=3, ColorId=2, DailyPrice=750, Description="7000Km Benzinli Premium", ModelYear=2022},
                new Car{Id=4, BrandId=3, ColorId=1, DailyPrice=500, Description="10000KM Benzinli Otomatik", ModelYear=2022},
                new Car{Id=5, BrandId=1, ColorId=1, DailyPrice=850, Description="5000Km Dizel Otomatik Premium", ModelYear=2022}
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car CarToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(CarToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetByBrandId(int brandId)
        {
            return _cars.Where(c => c.BrandId == brandId).ToList();
        }
        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car CarToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            CarToDelete.BrandId = car.BrandId;
            CarToDelete.ColorId = car.ColorId;
            CarToDelete.DailyPrice = car.DailyPrice;
            CarToDelete.Description = car.Description;
            CarToDelete.ModelYear = car.ModelYear;
            
        }
    }
}
