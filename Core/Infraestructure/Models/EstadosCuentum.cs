using System;
using System.Collections.Generic;

namespace Core.Infraestructure.Models;

public partial class EstadosCuentum
{
    public int EstadoCuentaId { get; set; }

    public int TarjetaId { get; set; }

    public int Mes { get; set; }

    public int Anio { get; set; }

    public decimal SaldoMesAnterior { get; set; }

    public decimal SaldoMesActual { get; set; }

    public decimal InteresBonificable { get; set; }

    public decimal PagoMinimo { get; set; }

    public decimal PagoContadoConInteres { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual TarjetasCredito Tarjeta { get; set; } = null!;
}
