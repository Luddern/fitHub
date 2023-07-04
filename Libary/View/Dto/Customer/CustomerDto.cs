using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.Libary.View.Dto
{
    public class CustomerDto
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }

    }
    public class CustomerPageDto
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
        public List<CustomerDto> Items { get; set; }
    }
}