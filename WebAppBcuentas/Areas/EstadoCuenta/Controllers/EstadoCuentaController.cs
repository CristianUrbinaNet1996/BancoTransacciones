using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppBcuentas.Areas.EstadoCuentas.Interfaces;

namespace WebAppBcuentas.Areas.EstadoCuentas.Controllers
{
    [Area("EstadoCuenta")]
    public class EstadoCuentaController : Controller
    {
        private readonly IEstadoCuenta _estadoCuenta;

        public EstadoCuentaController(IEstadoCuenta estadoCuenta)
        {
            _estadoCuenta = estadoCuenta;
        }


        public async Task<IActionResult> Index()
        {
            var EstadoCuenta = await _estadoCuenta.GetEstadoCuentaByTarjeta(1);
            return View(EstadoCuenta);
        }


        [HttpGet("GetEstadoCuenta/{idTarjeta}")]
        public async Task<IActionResult> GetEstadoCuenta(int idTarjeta)
        {
            var EstadoCuenta = await _estadoCuenta.GetEstadoCuentaByTarjeta(idTarjeta);
            return View(EstadoCuenta);
        }

    }
}
