using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public IResult Add(Rental rental)
        {
            //Validations Derslerine daha geçmediğimiz için
            //Spagetti kontrol yazıyorum
            if (this.GetAllByCarId(rental.CarId).Data.Count != 0)
            {
                foreach (var item in this.GetAllByCarId(rental.CarId).Data)
                {
                    if (!item.ReturnDate.HasValue)
                    {
                        return new ErrorResult(Messages.TaskFail);
                    }
                }
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.TaskSuccess);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.TaskSuccess);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>
                (_rentalDal.GetAll(), Messages.TaskSuccess);
        }

        public IDataResult<List<Rental>> GetAllByCarId(int id)
        {
            return new SuccessDataResult<List<Rental>>
                (_rentalDal.GetAll(r => r.CarId == id), Messages.TaskSuccess);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>
                (_rentalDal.Get(r => r.RentalId == id), Messages.TaskSuccess);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.TaskSuccess);
        }
    }
}
