﻿using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelper
    {
        //Kayıt işlemi yapılacak klasörü directory olarak gönderiyorum
        //bunun sebebi resim değil başka bir file yüklenecek olursa fileHelper
        //O file ı resimlere değil başka klasörlere yükleyebilir
        public static IResult Upload(IFormFile file, string directory)
        {
            var fileType = Path.GetExtension(file.FileName);

            if (!CheckIfFileExists(file))
            {
                return new ErrorResult();
            }
            if (!CheckIfFileTypeValid(fileType))
            {
                return new ErrorResult();
            }
            CheckIfDirectoryExists(directory);
            var result = CreateImageFile(file, directory, fileType);
            return new SuccessResult(result);
        }

        public static IResult Update(IFormFile file, string filePath, string directory)
        {
            var fileType = Path.GetExtension(file.FileName);

            if (!CheckIfFileExists(file))
            {
                return new ErrorResult();
            }
            if (!CheckIfFileTypeValid(fileType))
            {
                return new ErrorResult();
            }
            DeleteOldFile(filePath);
            CheckIfDirectoryExists(directory);
            var result = CreateImageFile(file, directory, fileType);
            return new SuccessResult(result);
        }

        public static IResult Delete(string filePath)
        {
            DeleteOldFile(filePath);
            return new SuccessResult();
        }





        //------------------------------------------------------//

        private static bool CheckIfFileExists(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                return true;
            }
            return false;
        }

        private static bool CheckIfFileTypeValid(string fileType)
        {
            if (fileType != ".jpg" && fileType != ".jpeg" && fileType != ".png")
            {
                return false;
            }
            return true;
        }

        private static void CheckIfDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static string CreateImageFile(IFormFile file, string directory, string fileType)
        {
            string guid = Guid.NewGuid().ToString();

            using (FileStream fileStream = File.Create(directory + guid + fileType))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return (directory + guid + fileType);
        }

        private static void DeleteOldFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


    }
}