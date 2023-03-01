using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ServiceRespone
{
    public class ServiceRespone<T> where T : class
    {
        
        public string returnCode { get; set; }
        public string errorMsg { get; set; }
        public T result { get; set; }

    }
}
