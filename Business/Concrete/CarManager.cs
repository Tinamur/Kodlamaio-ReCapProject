﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        public IResult Add(Car car)
        {
            //Normalde Buraya ValidationManager
            //Bağlayıp onu kullanmak daha iyi olur
            //Ancak Henüz Kampta O Derslerde veya Ödevlerde
            //Değiliz
            if (car.CarName.Length < 2 && car.DailyPrice! > 0)
            {
                return new ErrorResult(Messages.TaskFail);
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);

        }
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>
                (_carDal.Get(c => c.CarId == id), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>
                (_carDal.GetAll(), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetAllByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>
                (_carDal.GetAll(c => c.BrandId == id), Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>
                (_carDal.GetAll(c => c.ColorId == id), Messages.CarsListed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>
                (_carDal.GetCarDetails(), Messages.CarDetailsListed);
        }
    }
}
