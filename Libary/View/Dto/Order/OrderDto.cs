using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.Libary.View.Dto
{
    public class OrderDto
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesName { get; set; }
        
    }
    public class OrderPageDto
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
        public List<OrderDto> Items { get; set; }
        
    }
}