using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.Libary.View.Dto;

namespace FitHub.Service
{
    public interface IOrderService
    {
        Result<string> AddOrder(string customerId, decimal totalAmount, int status, DateTime OrderDate, string salesName);

        Result<string> UpdateOrder(string orderId, string customerId, decimal totalAmount, int status, DateTime OrderDate, string salesName);

        Result<string> DelOrder(string OrderId);

        Result<OrderDto> GetOrder(string OrderId);

        Result<OrderPageDto> GetOrderList(int pageNo, int pageSize);
       Result<OrderPageDto> GetFilterOrderList(string orderId, string customerId, int status, bool isTimeOver, DateTime OrderDate, string salesName,int pageNo, int pageSize);


    }
}