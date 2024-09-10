using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Enums
{
    public static class TipoTransaccion
    {
        public enum TipoTransacción
        {
            [Display(Name = "Pago")]
            Pago = 1,
            [Display(Name = "Compra")]
            Compra = 2,
            [Display(Name = "Reversion")]
            Reversion = 3,

        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
        }
    }
}
