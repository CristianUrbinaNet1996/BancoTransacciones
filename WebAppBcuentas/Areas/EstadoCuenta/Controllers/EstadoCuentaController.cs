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


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("GetEstadoCuenta/{idTarjeta}")]
        public async Task<IActionResult> GetEstadoCuenta(int idTarjeta)
        {
            var EstadoCuenta = await _estadoCuenta.GetEstadoCuentaByTarjeta(idTarjeta);
            return View(EstadoCuenta);
        }

    }
}
