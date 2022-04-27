using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
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

        public IResult Add(IFormFile file, CarImage carImage)
        {
            var imageLimitResult = CheckIfCarImageLimitExceeded(carImage.CarId);
            if (!imageLimitResult.Success)
            {
                return imageLimitResult;
            }
            var carImageDirectory = PathConstants.CarImagesMainPath + "" + carImage.CarId + "\\";
            var imageUploadResult = FileHelper.Upload(file, carImageDirectory);
            if (!imageUploadResult.Success)
            {
                return imageUploadResult;
            }
            carImage.ImagePath = imageUploadResult.Message;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var carImagePath = PathConstants.CarImagesMainPath + "" + carImage.CarId + "\\" + carImage.ImagePath;
            FileHelper.Delete(carImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>
                (_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int id)
        {
            var result = CheckIfCarImageNull(id);
            if (!result.Success)
            {
                var defaultImages = GenerateDefaultImages(id);
                return defaultImages;
            }

            return new SuccessDataResult<List<CarImage>>
                (_carImageDal.GetAll(c => c.CarId == id));
        }

        //GetById probably does nothing for now
        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>
                (_carImageDal.Get(c => c.CarImageId == id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            var carImageDirectory = PathConstants.CarImagesMainPath + "" + carImage.CarId + "\\";
            var carImagePath = carImageDirectory + carImage.ImagePath;
            FileHelper.Update(file, carImagePath, carImageDirectory);
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        // default için car id yi 0 yapabilirsin işe yaramazsa
        // default image i pathconstants a yazdım ayrıca image klasörü içinde de file olarak var
        // default image in type i .jpeg ve path constant da gösterdim sorun çıkabilir denemek lazım
        // Normalde filepath üzerine directory yi yazmadın ancak onu değiştir ve filepath içinde herşey olsun
        //-------------------------------------------------------------//
        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            var maxAllowedImageCount = 5;
            var carImageCount = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (carImageCount >= maxAllowedImageCount)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded + "" + maxAllowedImageCount);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarImageNull(int id)
        {
            var carImages = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!carImages)
            {
                return new ErrorResult();
            }
            return new SuccessResult();

        }


        private IDataResult<List<CarImage>> GenerateDefaultImages(int id)
        {
            string imagePath = PathConstants.DefaultCarImage;
            List<CarImage> defaultImages = new List<CarImage>();
            defaultImages.Add(new CarImage { CarId = id, ImagePath = imagePath });
            return new SuccessDataResult<List<CarImage>>(defaultImages);
        }

    }
}
