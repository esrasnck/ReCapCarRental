using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
    public class FileUploadHelper  //todo: burada ya ya fena patlayacaz. ya da çıkacaz :(  Sen bir kal  => burada sıkıntı büyük :(
    {
        //rootPath = sonrasında _enviroment.webRootPath atanacak
        //public static string FileAdd(IFormFile formFile, string rootPath) // dosya yoluna ihtiyacım var. string dönüyor.
        //{
        //    string webImagePath = "";

        //    if (formFile.Length > 0)
        //    {
        //        string imageExtension = Path.GetExtension(formFile.FileName);
        //        string newPicName = string.Format($"{Guid.NewGuid().ToString("D")}{imageExtension}");
        //        string imageFolder = Path.Combine(rootPath, "Images");
        //        string fullImagePath = Path.Combine(imageFolder, newPicName);
        //        webImagePath = string.Format($"/Images/{newPicName}");
        //        if (!Directory.Exists(imageFolder))
        //        {
        //            Directory.CreateDirectory(imageFolder);
        //        }

        //        using (FileStream fileStream = System.IO.File.Create(fullImagePath))
        //        {
        //            formFile.CopyTo(fileStream);
        //            fileStream.Flush();
        //        }
        //    }


        //    return webImagePath;
        //}


        //public static (string newPath, string Path2) newPath(IFormFile file)
        //{
        //    System.IO.FileInfo file_Info = new System.IO.FileInfo(file.FileName);

        //    string fileExtension = file_Info.Extension;

        //    var uniqFileName = Guid.NewGuid().ToString("N") + fileExtension;

        //    string path = Environment.CurrentDirectory + @"\wwwroot\Images\";

          

        //    string result = $@"{path}{uniqFileName}";
            
        //    return (result, $"\\Images\\{uniqFileName}");


        //    // C:\Users\Esra SANCAK\source\repos\ReCapCarRental\WebAPI\wwwroot\Images\
        //}

        public static string Add(IFormFile file)
        {
            var sourcePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(sourcePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            }

            var result = NewPath(file);
            File.Move(sourcePath, result);
            return result;
        }

        public static string NewPath(IFormFile file)
        {
            FileInfo file_info = new FileInfo(file.FileName);
            string file_extension = file_info.Extension;

            string path = Environment.CurrentDirectory + @"\wwwroot\Images";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var new_path = Guid.NewGuid().ToString("N") + "_" + file_extension;
            string result = $@"{path}\{new_path}";
            return result;
        }

        public static string Update(string sourcePath, IFormFile file)
        {
            var result = NewPath(file);
            if (sourcePath.Length>0)
            {
                using (var stream = new FileStream(result, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            File.Delete(sourcePath);
            return result;
        }

        public static void Delete(string path)
        {
            // todo : dön bak sonra :)

            File.Delete(path);
        }
    }
}
