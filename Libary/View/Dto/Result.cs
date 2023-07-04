using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.Libary.View.Dto
{
    public class Result<T> where T : class
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public T Items { get; set; }

    }
}