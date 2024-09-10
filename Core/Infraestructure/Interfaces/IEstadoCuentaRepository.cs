using DTO.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Interfaces
{
    public interface IEstadoCuentaRepository
    {

        Task<EstadosCuentaDto> GetEstadodeCuentaByTarjetaAndRangeDates(int? TarjetaId,DateTime? fechaInicio=null,DateTime? fechaFin=null);
     
    }
}
