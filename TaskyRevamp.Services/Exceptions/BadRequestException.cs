using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Services.Exceptions
{
    public abstract class BadRequestException : ApplicationException
    {
        public BadRequestException(string message)
            : base("Bad Request", message, StatusCodes.Status400BadRequest)
        {
        }
    }
}
