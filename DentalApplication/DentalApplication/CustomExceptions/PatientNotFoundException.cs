using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApplication.CustomExceptions
{
    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException()
        {
        }

        public PatientNotFoundException(string message)
            : base(message)
        {
        }

        public PatientNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}