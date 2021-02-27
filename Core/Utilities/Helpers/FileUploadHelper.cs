using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;

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


        public static (string newPath, string Path2) newPath(IFormFile file)
        {
            System.IO.FileInfo file_Info = new System.IO.FileInfo(file.FileName);

            string fileExtension = file_Info.Extension;

            var uniqFileName = Guid.NewGuid().ToString("N") + fileExtension;

            string path = Environment.CurrentDirectory + @"\wwwroot\Images\";

          

            string result = $@"{path}{uniqFileName}";
            
            return (result, $"\\Images\\{uniqFileName}");


            // C:\Users\Esra SANCAK\source\repos\ReCapCarRental\WebAPI\wwwroot\Images\
        }

        public static string Add(IFormFile file)
        {
            var result = newPath(file);

            var sourePath = Path.GetTempPath();
            if (file.Length > 0)
            {
                using (var stream = new FileStream(sourePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


            }
            File.Move(sourePath, result.newPath);

            return result.Path2;
        }
    }
}
