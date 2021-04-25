using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleViews.Exceptions
{
    public class DisplayBoxOverlapException : Exception
    {
        public DisplayBoxOverlapException()
            : base(String.Format("DisplayBox bounds overlap with another already added to screen")) { }

    }
}
