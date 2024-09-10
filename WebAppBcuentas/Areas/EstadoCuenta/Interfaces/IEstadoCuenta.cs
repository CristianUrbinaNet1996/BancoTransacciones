using WebAppBcuentas.Areas.EstadoCuentas.Models;

namespace WebAppBcuentas.Areas.EstadoCuentas.Interfaces
{
    public interface IEstadoCuenta
    {
        Task<EstadoCuentaVM> GetEstadoCuentaByTarjeta(int Id);
    }
}
