using System;
using System.Collections.Generic;

namespace Core.Infraestructure.Models;

public partial class ParametrosConfiguracion
{
    public int ParametroId { get; set; }

    public decimal PorcentajeInteres { get; set; }

    public decimal PorcentajePagoMinimo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<TarjetasCredito> TarjetasCreditos { get; } = new List<TarjetasCredito>();
}
