
using DTO.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.DTO.Models
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public DateTime? FechaNacimiento { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<TarjetasCreditoDto> TarjetasCreditos { get; } = new List<TarjetasCreditoDto>();
    }
}
