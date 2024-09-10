using DTO.DTO.Models;
using DTO.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Interfaces
{
    public interface ITransaccionesRepository
    {
        Task<List<TransaccionesDTO>> GetTransaccionesByIdTarjeta(int tarjetaId); 
        Task<TransaccionesDTO> CreateTransaccion(CreateTransaccionRequestDto transaccion);
    }
}
