using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mitc_report_Update.Extensions.Exceptions
{
    public class RazorEngineException : Exception
    {
#pragma warning disable CS0114
        public string Message { get; set; }
        public RazorEngineException(string message)
    : base(message) { }


        public RazorEngineException(string message, Exception innerException)
            : base(message, innerException) { }


    }
}