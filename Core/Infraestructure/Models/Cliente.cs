using System;
using System.Collections.Generic;

namespace Core.Infraestructure.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<TarjetasCredito> TarjetasCreditos { get; } = new List<TarjetasCredito>();
}
