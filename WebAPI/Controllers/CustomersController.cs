using Bussiness.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _custormerService;

        public CustomersController(ICustomerService customerService)
        {
            _custormerService = customerService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _custormerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetCustomerDetail()
        {
            var result = _custormerService.GetCustomerDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var result = _custormerService.GetByID(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult CustomerAdd([FromBody] Customer customer)
        {
            var result = _custormerService.AddACustomer(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult CustomerUpdate([FromBody]Customer customer)
        {
            var result = _custormerService.UpdateCustomer(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult CustomerDelete([FromBody]Customer customer)
        {
            var result = _custormerService.DeleteCustomer(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


    }
}
