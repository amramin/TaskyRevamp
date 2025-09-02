using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskyRevamp.Services.Exceptions
{
    public sealed class ValidationException : ApplicationException
    {
        public ValidationException(Dictionary<string, List<string>> errorsDictionary)
            : base("Validation Failure", "One or more validation errors occurred", StatusCodes.Status422UnprocessableEntity)
            => ErrorsDictionary = errorsDictionary;

        public Dictionary<string, List<string>> ErrorsDictionary { get; }
    }
}
