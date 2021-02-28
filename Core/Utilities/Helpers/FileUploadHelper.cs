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
        // 
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
