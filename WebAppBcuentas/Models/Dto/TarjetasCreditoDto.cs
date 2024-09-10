using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppBcuentas.Models.Dto
{
    public class TarjetasCreditoDto
    {
        public int TarjetaId { get; set; }

        public int ClienteId { get; set; }

        public string Cliente { get; set; }

        public string NumeroTarjeta { get; set; } = null!;

        public DateTime FechaExpiracion { get; set; }

        public string Cvv { get; set; } = null!;

        public decimal LimiteCredito { get; set; }

        public decimal SaldoActual { get; set; }

        public decimal? SaldoDisponible { get; set; }

        public string Estado { get; set; } = null!;

        public DateTime? FechaCreacion { get; set; }

        public int? Configuracion { get; set; }
        public virtual ParametrosConfiguracionDto? ConfiguracionNavigation { get; set; }
        public virtual ICollection<EstadosCuentaDto> EstadosCuenta { get; } = new List<EstadosCuentaDto>();

        public virtual ICollection<TransaccionesDTO> Transacciones { get; } = new List<TransaccionesDTO>();
    }
}
