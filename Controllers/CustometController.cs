using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.Libary.View.Dto;
using FitHub.Libary.View.ViewModel;
using FitHub.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FitHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _custometService;
        public CustomerController(ICustomerService custometService)
        {
            _custometService = custometService;
        }
        [HttpPost]
        [SwaggerResponse(200, "成功", typeof(Result<string>))]
        public IActionResult AddCustomer([FromBody] AddCustomerViewModel model)
        {
            var result = _custometService.AddCustomer(model.Name, model.Address, model.City, model.State, model.Country, model.Zip);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }

        }
        [HttpPut]
        [SwaggerResponse(200, "成功", typeof(Result<string>))]
        public IActionResult UpdateCustomer([FromBody] UpdateCustomerViewModel model)
        {
            var result = _custometService.UpdateCustomer(model.CustomerId, model.Name, model.Address, model.City, model.State, model.Country, model.Zip);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }

        }
        [HttpDelete("{customerId}")]
        [SwaggerResponse(200, "成功", typeof(Result<string>))]
        public IActionResult DelCustomer(string customerId)
        {
            var result = _custometService.DelCustomer(customerId);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }


        }
        [HttpGet("{customerId}")]
        [SwaggerResponse(200, "成功", typeof(Result<CustomerDto>))]
        public IActionResult GetCustomer(string customerId)
        {
            var result = _custometService.GetCustomer(customerId);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }

        }
        [HttpGet("list")]
        [SwaggerResponse(200, "成功", typeof(Result<CustomerPageDto>))]
        public IActionResult GetCustomerList(int pageNo, int pageSize)
        {
            var result = _custometService.GetCustomerList(pageNo, pageSize);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
        [HttpGet("filter")]
        [SwaggerResponse(200, "成功", typeof(Result<List<CustomerDto>>))]
        public IActionResult GetFilterCustometList(string name, string address, string city, string state, string country, string zip, int pageNo, int pageSize)
        {
            var result = _custometService.GetFilterCustomerList(name, address, city, state, country, zip, pageNo, pageSize);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

    }
}