using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.Libary.View.ViewModel
{
    public class AddOrderViewModel
    {

        public string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesName { get; set; }
    }
    public class UpdateOrderViewModel
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesName { get; set; }

    }
}