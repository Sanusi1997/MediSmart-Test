using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediSmart.API.Data.DTO
{
    public class ResponseDTO
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public ResponseDTO(string message, bool status)
        {
            this.Status = status;
            this.Message = message ??
            throw new ArgumentNullException(nameof(Message));
        }

    }
}
