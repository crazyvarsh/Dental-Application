using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.CustomExceptions
{
    public class DentistNotFoundException : Exception
    {
        public DentistNotFoundException()
        {
        }

        public DentistNotFoundException(string message)
            : base(message)
        {
        }

        public DentistNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}