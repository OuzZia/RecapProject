using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public static class FileHelper
    {
        private static string _currentFileDirectory = Environment.CurrentDirectory + "//wwwroot";
        private static string _folderName = "//Images//";

        public static IResult Add(IFormFile file)
        {
            var fileExists = CheckFileExists(file);
            if (!fileExists.Success)
            {
                return new ErrorResult(fileExists.Message);
            }

            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);

            if (!typeValid.Success)
            {
                return new ErrorResult(typeValid.Message);
            }

            var randomGuid = Guid.NewGuid().ToString().Replace("-", "");

            var directory = _currentFileDirectory + _folderName;
            var fileDirectory = directory + randomGuid + type;

            CheckFileDirectoryExists(directory);
            CreateImageFile(fileDirectory, file);

            var fileAddressToBeSavedOnDatabase = _folderName + randomGuid + type;
            return new SuccessResult(fileAddressToBeSavedOnDatabase.Replace("\\", "/"));
        }

        public static IResult Update(IFormFile file, string imagePath)
        {
            var fileExists = CheckFileExists(file);
            if (!fileExists.Success)
            {
                return new ErrorResult(fileExists.Message);
            }
            var type = Path.GetExtension(file.FileName);
            var typeValid = CheckFileTypeValid(type);
            if (!typeValid.Success)
            {
                return new ErrorResult(typeValid.Message);
            }
            var randomGuid = Guid.NewGuid().ToString().Replace("-", "");

            var directory = _currentFileDirectory + _folderName;
            var fileDirectory = directory + randomGuid + type;

            DeleteOldImageFile(imagePath.Replace("/", "\\"));
            CheckFileDirectoryExists(directory);
            CreateImageFile(fileDirectory, file);

            var fileAddressToBeSavedOnDatabase = _folderName + randomGuid + type;
            return new SuccessResult(fileAddressToBeSavedOnDatabase.Replace("\\", "/"));
        }
        public static IResult Delete(string path)
        {
            DeleteOldImageFile(path.Replace("/", "\\"));
            return new SuccessResult();
        }

        private static IResult CheckFileTypeValid(string type)
        {
            if (type == ".jpeg" || type == ".png" || type == ".jpg")
            {
                return new SuccessResult();
            }
            return new ErrorResult("Dosya tipi desteklenmiyor.(.jpeg, .png, .jpg olmalı)");
        }

        private static IResult CheckFileExists(IFormFile file)
        {
            if (file!=null && file.Length>0)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Dosya bulunamıyor.");
        }

        private static void DeleteOldImageFile(string directory)
        {
            var fullDirectory = Environment.CurrentDirectory + "\\wwwroot" + directory;
            if (File.Exists(fullDirectory))
            {
                File.Delete(fullDirectory);
            }
        }

        private static void CheckFileDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private static void CreateImageFile(string directory, IFormFile file)
        {
            using (FileStream  fileStream = File.Create(directory))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }
}
