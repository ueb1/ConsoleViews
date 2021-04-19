using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleViews.Exceptions
{
    public class InvalidBorderException : Exception
    {
        public InvalidBorderException(string message) : base(message){}
    }
}
