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

         
        }
 
    }
}
