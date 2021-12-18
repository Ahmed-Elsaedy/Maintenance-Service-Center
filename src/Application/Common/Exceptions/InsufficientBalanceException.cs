using System;
using System.Collections.Generic;
using System.Text;

namespace ElarabyCA.Application.Common.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base()
        {

        }
        public InsufficientBalanceException(string message) : base(message)
        {

        }
    }
}
