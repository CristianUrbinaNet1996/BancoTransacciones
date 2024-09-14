using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppBcuentas.Areas.Cliente.Interfaces;
using WebAppBcuentas.Areas.Transacciones.Interface;
using WebAppBcuentas.Models.Dto;

namespace WebAppBcuentas.Areas.Transacciones.Controllers
{
    [Area("Transacciones")]
    public class TransaccionesController : Controller
    {

        private ITransaccion _transaccionService;
        private ICliente _clienteService;
        private IMapper _mapper;

        public TransaccionesController(ITransaccion transaccionService,IMapper mapper,ICliente cliente)
        {
            _transaccionService = transaccionService;
            _clienteService = cliente;
            _mapper = mapper;   
        }
        // GET: Transaccion
        public async Task<ActionResult> GetTransaccionesByIdTarjeta(int idTarjeta)
        {
            var transacciones = await _transaccionService.TransaccionesByidTarjeta(idTarjeta);

            return View(transacciones);
        }
        [HttpGet]
        public async Task<IActionResult> AgregarTransaccion()
        {

            var Result = await _clienteService.GetAll();
            ViewData["Clientes"] = _mapper.Map<List<SelectListItem>>(Result);

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AgregarTransaccion([FromForm] TransaccionesDTO transaccion)
        {

            var Result = await _clienteService.GetAll();
            ViewData["Clientes"] = _mapper.Map<List<SelectListItem>>(Result);

            var tran = await _transaccionService.AddTransaccion(transaccion);

            return View(tran);

        }


    }
}
