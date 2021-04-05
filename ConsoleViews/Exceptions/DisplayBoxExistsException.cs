using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestiLeikurinn.Exceptions
{
    public class DisplayBoxExistsException : Exception
    {
        public DisplayBoxExistsException(string name)
            : base(String.Format("DisplayBox with the same name {0} has already been added to screen.", name))
        {

        }

    }
}
