using System;
using System.Collections.Generic;

namespace Core.Infraestructure.Models;

public partial class TarjetasCredito
{
    public int TarjetaId { get; set; }

    public int ClienteId { get; set; }

    public string NumeroTarjeta { get; set; } = null!;

    public DateTime FechaExpiracion { get; set; }

    public string Cvv { get; set; } = null!;

    public decimal LimiteCredito { get; set; }

    public decimal SaldoActual { get; set; }

    public decimal? SaldoDisponible { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public int? Configuracion { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ParametrosConfiguracion? ConfiguracionNavigation { get; set; }

    public virtual ICollection<EstadosCuentum> EstadosCuenta { get; } = new List<EstadosCuentum>();

    public virtual ICollection<Transaccione> Transacciones { get; } = new List<Transaccione>();
}
