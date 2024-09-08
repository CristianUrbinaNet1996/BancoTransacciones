using System;
using System.Collections.Generic;

namespace Core.Infraestructure.Models;

public partial class Transaccione
{
    public int TransaccionId { get; set; }

    public int TarjetaId { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Monto { get; set; }

    public string TipoTransaccion { get; set; } = null!;

    public virtual TarjetasCredito Tarjeta { get; set; } = null!;
}
