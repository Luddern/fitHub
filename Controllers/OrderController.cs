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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [SwaggerResponse(200, "成功", typeof(Result<string>))]
        public IActionResult AddOrder([FromBody] AddOrderViewModel model)
        {
            var result = _orderService.AddOrder(model.CustomerId, model.TotalAmount, model.Status, model.OrderDate, model.SalesName);
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
        public IActionResult UpdateOrder([FromBody] UpdateOrderViewModel model)
        {
            var result = _orderService.UpdateOrder(model.OrderId, model.CustomerId, model.TotalAmount, model.Status, model.OrderDate, model.SalesName);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }


        }
        [HttpDelete("{orderId}")]
        [SwaggerResponse(200, "成功", typeof(Result<string>))]
        public IActionResult DelOrder(string orderId)
        {
            var result = _orderService.DelOrder(orderId);
            if (result.StatusCode == 200)
            {
                return Ok(result.Items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
        [HttpGet("{orderId}")]
        [SwaggerResponse(200, "成功", typeof(Result<OrderDto>))]
        public IActionResult GetOrder(string orderId)
        {
            var result = _orderService.GetOrder(orderId);
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
        [SwaggerResponse(200, "成功", typeof(Result<OrderPageDto>))]
        public IActionResult GetOrderList(int pageNo, int pageSize)
        {
            var result = _orderService.GetOrderList(pageNo, pageSize);
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
        [SwaggerResponse(200, "成功", typeof(Result<List<OrderDto>>))]
        public IActionResult GetFilterOrderList(string orderId, string customerId, int status, bool isTimeOver, DateTime OrderDate, string salesName,int pageNo,int pageSize)
        {
            var result = _orderService.GetFilterOrderList(orderId, customerId, status, isTimeOver, OrderDate, salesName,pageNo,pageSize);
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