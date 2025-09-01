using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Services.Exceptions
{
    public class CannotCreateException : ApplicationException
    {
        public CannotCreateException(string message) : base("Cannot Create", message, StatusCodes.Status400BadRequest)
        {
        }
    }
}
