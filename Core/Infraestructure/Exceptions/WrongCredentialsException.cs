using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Exceptions
{
    [Serializable]
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException(string message):base(message) { }
    }
}
