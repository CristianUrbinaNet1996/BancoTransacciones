using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.DTO
{
    public class BaseResponse<T>
    {
        public bool? Status { get; set; }
        public string? Message { get; set; }
        public string? DebugMessage { get; set; }
        public string? MapId { get; set; }
        public T? Data { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(T? result)
        {
            Status = true;
            Message = "OK";
            Data = result;
        }

        public BaseResponse(string? message)
        {
            Status = false;
            Message = message;
        }
    }
}
