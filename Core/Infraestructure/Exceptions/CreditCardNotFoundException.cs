using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Exceptions
{
    [Serializable]
    public class CreditCardNotFoundException :Exception

    {
        public int CardNumber { get; }
      

        public CreditCardNotFoundException(string message, int cardNumber)
        : base(message)
        {
            CardNumber = cardNumber;
        
        }
    }
}
