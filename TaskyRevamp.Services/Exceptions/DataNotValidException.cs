using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Services.Exceptions
{
    public class DataNotValidException : ApplicationException
    {
        public DataNotValidException(string message) : base("Not Valid", message, StatusCodes.Status406NotAcceptable)
        {
        }
    }
}
