using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leafy.Application.Utils
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Detail { get; set; }

        public ErrorModel(int StatusCode, string? Message, string? Detail = null) 
        { 
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.Detail = Detail;
        }
    }
}
