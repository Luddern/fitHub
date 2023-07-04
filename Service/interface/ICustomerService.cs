using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitHub.Libary.View.Dto;

namespace FitHub.Service
{
    public interface ICustomerService
    {
        Result<string> AddCustomer(string name, string address, string city, string status, string country, string zip);
        Result<string> UpdateCustomer(string custometId, string name, string address, string city, string status, string country, string zip);
        Result<string> DelCustomer(string custometId);
        Result<CustomerDto> GetCustomer(string customerId);
        Result<CustomerPageDto> GetCustomerList(int pageNo, int pageSize);
       Result<CustomerPageDto> GetFilterCustomerList(string name, string address, string city, string status, string country, string zip, int pageNo, int pageSize);



    }
}