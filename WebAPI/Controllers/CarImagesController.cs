using Bussiness.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        
        private ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
           
            _carImageService = carImageService;
        }

        [HttpGet]
        public IActionResult GetAllImages()
        {
            var result = _carImageService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult GetByImageId(int imageId)
        {
            var result = _carImageService.GetImageById(imageId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult GetCarListByCarId(int carId)
        {
            var result = _carImageService.GetByCarId(carId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult DeleteImage([FromBody] CarImage carImage)
        {
            var result = _carImageService.ImageDelete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult UpdateImage([FromForm] IFormFile file,[FromForm] CarImage carImage)
        {
           
            var result = _carImageService.ImageUpdate(file, carImage); // todo:yemeyebilir
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult AddImage([FromForm] IFormFile fromFormFiles, [FromForm] CarImage carImage)
        {
            var result = _carImageService.ImageAdd(fromFormFiles, carImage);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);

            #region eski
            //List<IResult> results = new List<IResult>();
            //foreach (IFormFile itemFormFileFile in fromFormFiles)
            //{
            //    if (itemFormFileFile.Length>0)
            //    {
            //        string imageExtension = Path.GetExtension(itemFormFileFile.FileName);
            //        string newPicName = string.Format($"{Guid.NewGuid().ToString("D")}{imageExtension}");
            //        string imageFolder = Path.Combine(_enviroment.WebRootPath, "Images");
            //        string fullImagePath = Path.Combine(imageFolder, newPicName);
            //        string webImagePath = string.Format($"/Images/{newPicName}");

            //        if (!Directory.Exists(imageFolder))
            //        {
            //            Directory.CreateDirectory(imageFolder);
            //        }

            //        using (FileStream fileStream =System.IO.File.Create(fullImagePath))
            //        {
            //           itemFormFileFile.CopyTo(fileStream);
            //           fileStream.Flush();
            //        }
            //        results.Add(_carImageService.ImageAdd(new CarImage()
            //        {
            //            ImagePath = webImagePath,
            //            CarId = carImage.CarId
            //        }));


            //    }
            //}

            //IResult check = BusinessRules.Run(results.ToArray());
            //if (check == null)
            //{
            //    return Ok(new SuccessResult("resim yüklendi"));
            //}
            //else
            //{
            //    return BadRequest(results);
            //}
#endregion
        }
 
    }
}
