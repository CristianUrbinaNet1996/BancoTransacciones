using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppBcuentas.Models.Dto
{
    public class ParametrosConfiguracionDto
    {
        public int ParametroId { get; set; }

        public decimal PorcentajeInteres { get; set; }

        public decimal PorcentajePagoMinimo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string Estado { get; set; } = null!;


    }
}
