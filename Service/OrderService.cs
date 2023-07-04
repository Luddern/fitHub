using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.Libary.DataBase.Context;
using FitHub.Libary.DataBase.Entities;
using FitHub.Libary.View.Dto;
using LinqToDB;

namespace FitHub.Service
{
    public class OrderService : IOrderService
    {
        private readonly FitHubContext _dbContext;
        public OrderService(FitHubContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<string> AddOrder(string customerId, decimal totalAmount, int status, DateTime OrderDate, string salesName)
        {
            var result = new Result<string>();
            try
            {
                var order = new Order()
                {
                    Id = Guid.NewGuid().ToString().Substring(0, 15),
                    CustomerId = customerId,
                    TotalAmount = totalAmount,
                    Status = status,
                    OrderDate = OrderDate,
                    SalesName = salesName
                };
                _dbContext.Order.Add(order);
                _dbContext.SaveChanges();
                result.Items = order.Id;
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.StatusCode = 500;
            }
            return result;
        }
        public Result<string> UpdateOrder(string orderId, string customerId, decimal totalAmount, int status, DateTime OrderDate, string salesName)
        {
            var result = new Result<string>();
            try
            {
                var order = _dbContext.Order.FirstOrDefault(x => x.Id == orderId);
                if (order == null)
                {
                    result.ErrorMessage = "data is not exist";
                    return result;
                }
                order.CustomerId = customerId;
                order.TotalAmount = totalAmount;
                order.Status = status;
                order.OrderDate = OrderDate;
                order.SalesName = salesName;
                _dbContext.Update(order);
                _dbContext.SaveChanges();

                result.Items = order.Id;
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.StatusCode = 500;
            }
            return result;
        }
        public Result<string> DelOrder(string OrderId)
        {
            var result = new Result<string>();
            try
            {
                var order = _dbContext.Order.FirstOrDefault(x => x.Id == OrderId);
                if (order == null)
                {
                    result.ErrorMessage = "data is not exist";
                    return result;
                }
                _dbContext.Order.Remove(order);
                _dbContext.SaveChanges();
                result.Items = order.Id;
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.StatusCode = 500;
            }
            return result;
        }
        public Result<OrderDto> GetOrder(string OrderId)
        {
            var result = new Result<OrderDto>();
            try
            {
                var order = _dbContext.Order.Where(x => x.Id == OrderId)
                .Join
                (
                    _dbContext.Customer,
                    order => order.CustomerId,
                    customer => customer.Id,
                    (order, customer) => new { order, customer }

                ).Select
                (
                    x =>
                    new OrderDto()
                    {
                        OrderId = x.order.Id,
                        CustomerId = x.order.CustomerId,
                        TotalAmount = x.order.TotalAmount,
                        Status = x.order.Status,
                        OrderDate = x.order.OrderDate,
                        SalesName = x.order.SalesName,
                        CustomerName = x.customer.Name
                    }
                ).FirstOrDefault();
                if (order == null)
                {
                    result.ErrorMessage = "data is not exist";
                    return result;
                }
                var orderDto = new List<OrderDto>();
                result.Items = order;
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.StatusCode = 500;
            }
            return result;
        }
        public Result<OrderPageDto> GetOrderList(int pageNo, int pageSize)
        {
            var result = new Result<OrderPageDto>();
            try
            {
                var order = _dbContext.Order
                .Join
                (
                    _dbContext.Customer,
                    order => order.CustomerId,
                    customer => customer.Id,
                    (order, customer) => new { order, customer }

                ).Select
                (
                    x =>
                    new OrderDto()
                    {
                        OrderId = x.order.Id,
                        CustomerId = x.order.CustomerId,
                        TotalAmount = x.order.TotalAmount,
                        Status = x.order.Status,
                        OrderDate = x.order.OrderDate,
                        SalesName = x.order.SalesName,
                        CustomerName = x.customer.Name
                    }
                );
                var total = order.Count();
                var totalPage = (int)Math.Ceiling((double)total / pageSize);
                if (total % pageSize != 0)
                {
                    totalPage++;
                }
                var orderDto = order.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                var orderPageDto = new OrderPageDto()
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    Total = total,
                    TotalPage = totalPage,
                    Items = orderDto
                };
                result.Items = orderPageDto;
                result.StatusCode = 200;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.StatusCode = 500;
            }
            return result;

        }
        public Result<OrderPageDto> GetFilterOrderList(string orderId, string customerId, int status, bool isTimeOver, DateTime OrderDate, string salesName, int pageNo, int pageSize)
        {
            Result<OrderPageDto> resultDto = new Result<OrderPageDto>();
            OrderPageDto result = new OrderPageDto();
            try
            {
                var order = _dbContext.Order.Where(
                    x => orderId.Contains(x.Id ?? string.Empty) || customerId.Contains(x.CustomerId ?? string.Empty) || x.Status == status ||
                    (isTimeOver ? x.OrderDate >= OrderDate : x.OrderDate <= OrderDate) || salesName.Contains(x.SalesName ?? string.Empty)
                )
                .Join
                (
                    _dbContext.Customer,
                    order => order.CustomerId,
                    customer => customer.Id,
                    (order, customer) => new { order, customer }

                ).Select
                (
                    x =>
                    new OrderDto()
                    {
                        OrderId = x.order.Id,
                        CustomerId = x.order.CustomerId,
                        TotalAmount = x.order.TotalAmount,
                        Status = x.order.Status,
                        OrderDate = x.order.OrderDate,
                        SalesName = x.order.SalesName,
                        CustomerName = x.customer.Name
                    }
                );
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.Items = order.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                result.Total = order.Count();
                result.TotalPage = (int)Math.Ceiling((double)result.Total / pageSize);
                resultDto.Items = result;
                resultDto.StatusCode = 200;
            }
            catch (Exception e)
            {
                resultDto.ErrorMessage = e.Message;
                resultDto.StatusCode = 500;
            }
            return resultDto;

        }





    }
}