using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Request
{
    public class CreateTransaccionRequestDto
    {
        public int TarjetaId { get; set; }

        public DateTime FechaTransaccion { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Monto { get; set; }

        public int? TipoTransaccion { get; set; }
    }
}
