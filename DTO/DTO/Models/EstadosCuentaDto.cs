﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Models
{
    public class EstadosCuentaDto
    {
        public int EstadoCuentaId { get; set; }

        public int TarjetaId { get; set; }

       public string Cliente { get; set; }

        public int Mes { get; set; }

        public int Anio { get; set; }

        public decimal SaldoMesAnterior { get; set; }

        public decimal SaldoMesActual { get; set; }

        public decimal InteresBonificable { get; set; }

        public decimal PagoMinimo { get; set; }

        public decimal PagoContadoConInteres { get; set; }

        public DateTime? FechaCreacion { get; set; }

     
    }
}
