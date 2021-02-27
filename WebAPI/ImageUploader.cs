using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebAPI  // bu kısım daha sonra ayarlanacak!
{
     public static class ImageUploader
    {
        // string değer döndürüyorum. Veri tabanına string ifade olarak kaydetmek için.
        // serverPath: resmimin hangi dosya yolunda olduğunu belirler.
        // IFormFile file : bana gelecek dosyanın tipi. "Http'ye göre postlanmış dosya verisi"
        //public static string UploadImage(string serverPath, IFormFile file)
        //{
        //    if (file!=null) // eger dosya geldiyse(resim varsa), null değilse, dosya isimleri karışmasın diye guid oluşturuyorum
        //    {

        //        Guid uniqName = Guid.NewGuid();

        //        serverPath = serverPath.Replace("~", string.Empty); // boşlukları tilda ile değiştirdik.(frontend için)
        //        string[] fileArray = file.FileName.Split('.'); // noktalardan file'ı ayırıp/array'e attık
        //        string extension = fileArray[fileArray.Length - 1].ToLower(); // dosya uzantısını yakalayarak, onu küçük harfe çevirdik.

        //        string fileName = $"{uniqName},{extension}"; // guid + extension'ımızı fileName e koyduk.

        //        if (extension=="jpq"|| extension =="gif"|| extension =="png"|| extension=="jpeg") // eğer bu dosya uzantısına sahipse
        //        {
        //            if (File.Exists(serverPath+fileName))
        //            {
        //                return "Dosya var";
        //            }
        //            else
        //            {
        //                var filePath = File.Create(serverPath + fileName);

        //            }
        //        }
        //    }
        //}
    }
}
