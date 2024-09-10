using WebAppBcuentas.Models.Dto;

namespace WebAppBcuentas.Areas.EstadoCuentas.Models
{
    public class EstadoCuentaVM
    {
        public int EstadoCuentaId { get; set; }

        public int TarjetaId { get; set; }

        public TarjetasCreditoDto Tarjeta {  get; set; }

        public ClienteDto Cliente { get; set; }

        public int Mes { get; set; }

        public int Anio { get; set; }

        public decimal SaldoMesAnterior { get; set; }

        public decimal SaldoMesActual { get; set; }

        public decimal InteresBonificable { get; set; }

        public decimal PagoMinimo { get; set; }

        public decimal PagoContadoConInteres { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<TransaccionesDTO> Transacciones { get; set; }

    }
}
