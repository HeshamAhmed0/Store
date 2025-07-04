using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Exceptions
{
    public class ValidationErrorResponse
    {
       public string ErrorMessage { get; set; } = "Validation Error";
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
        public IEnumerable<ValidationError> errors {  get; set; }

    }
}
