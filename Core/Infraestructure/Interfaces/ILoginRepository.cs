using Core.Infraestructure.DTO.Models;
using Core.Infraestructure.Models;
using DTO.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Interfaces
{
    public interface ILoginRepository
    {
        public ClienteDto Login(LoginDto formulario);
    }
}
