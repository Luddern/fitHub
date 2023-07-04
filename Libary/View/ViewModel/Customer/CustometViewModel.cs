using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.Libary.View.ViewModel
{
    public class AddCustomerViewModel
    {

        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }

    }
    public class UpdateCustomerViewModel
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }

    }
}