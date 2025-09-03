using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Services.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string title, string message, int statusCode)
            : base(message) {Title = title; StatusCode = statusCode; }
            

        public string Title { get; }
        public int StatusCode { get; set; } = 400;
    }
}
