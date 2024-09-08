using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Exceptions
{
    public class ClientNotFoundException:Exception
    {
        public int ClientId { get; }
        public ClientNotFoundException(string message, int clientId) : base(message) { 
        
        ClientId = clientId;
        }
    }
}
