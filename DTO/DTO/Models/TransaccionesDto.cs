using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Models
{
    public class TransaccionesDTO
    {
        public int TransaccionId { get; set; }

        public int TarjetaId { get; set; }

        public string NumeroTarjeta {  get; set; }

        public string Cliente {  get; set; }

        public DateTime FechaTransaccion { get; set; }

        public string Descripcion { get; set; } = null!;

        public decimal Monto { get; set; }

        public string TipoTransaccion { get; set; } = null!;

       
    }
}
