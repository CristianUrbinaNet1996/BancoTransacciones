using AutoMapper;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using DTO.DTO.Enums;
using DTO.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Repository
{
    public class EstadoCuentaRepository : IEstadoCuentaRepository
    {
        private BcuentasContext _context;
        private ITarjetaRepository _tarjetaRepository;
        private IClientesRepository _clientesRepository;
        private ITransaccionesRepository _transaccionesRepository;
        private IParametrosConfiguracionRepository _parametrosRepository;
        private IMapper _mapper;
        public EstadoCuentaRepository(BcuentasContext context, IClientesRepository clientesRepository, IParametrosConfiguracionRepository parametrosConfiguracion, ITarjetaRepository tarjetaRepository, IMapper mapper, ITransaccionesRepository transaccionesRepository)
        {

            _context = context;
            _clientesRepository = clientesRepository;
            _tarjetaRepository = tarjetaRepository;
            _mapper = mapper;
            _transaccionesRepository = transaccionesRepository;
            _parametrosRepository = parametrosConfiguracion;
        }
        
        public async Task<EstadosCuentaDto> GetEstadodeCuentaByTarjetaAndRangeDates(int? TarjetaId,DateTime? fechaInicio=null,DateTime? fechaFin = null)
        {
            // 1. Obtener la tarjeta de crédito
            var tarjeta = await _tarjetaRepository.Get(TarjetaId.Value);
            if (tarjeta == null)
            {
                throw new CreditCardNotFoundException("Tarjeta no encontrada", TarjetaId.Value);
            }

            // 2. Obtener el titular de la tarjeta (Cliente)
            var cliente = await _clientesRepository.GetClienteById(tarjeta.ClienteId);
            if (cliente == null)
            {
                throw new ClientNotFoundException("Cliente no encontrado",tarjeta.ClienteId);   
            }

            var transacciones = await _transaccionesRepository.GetTransaccionesByIdTarjeta(TarjetaId.Value);

            var transaccionesMesActual = new List<TransaccionesDTO>();
            if (fechaInicio is null)
            {
                fechaInicio = DateTime.Now;
                 transaccionesMesActual = transacciones
             .Where(t => t.FechaTransaccion.Month == fechaInicio.Value.Month || t.FechaTransaccion.Month == fechaInicio.Value.Month-1)
             .ToList();
            }
            else {
                 transaccionesMesActual = transacciones
             .Where(t => t.FechaTransaccion >= fechaInicio && t.FechaTransaccion <= fechaFin)
             .ToList();
            }
         

            // 4. Calcular los totales
            var saldoTotal = tarjeta.SaldoActual;
            var totalComprasMesActual = transaccionesMesActual.Sum(t =>(TipoTransaccion.TipoTransacción) t.TipoTransaccion == TipoTransaccion.TipoTransacción.Compra ? t.Monto : 0);

            var parametros = await _parametrosRepository.GetById(tarjeta.Configuracion.Value);

            var porcentajeInteres = parametros.PorcentajeInteres;
            var porcentajePagoMinimo = parametros.PorcentajePagoMinimo ;

         
            var interesBonificable = saldoTotal * (porcentajeInteres / 100);
            var cuotaMinima = saldoTotal * (porcentajePagoMinimo / 100);
            var totalConIntereses = saldoTotal + interesBonificable;

            // 7. Mapeo de resultado con AutoMapper
            var estadoCuentaDto = new EstadosCuentaDto
            {
                TarjetaId = tarjeta.TarjetaId,
                Cliente = cliente,
                NumeroTarjeta=tarjeta.NumeroTarjeta,
                Mes = fechaInicio.Value.Month,
                Anio = fechaInicio.Value.Year,
                SaldoMesAnterior = 0, 
                SaldoMesActual = saldoTotal,
                SaldoDisponible = tarjeta.SaldoDisponible,
                InteresBonificable = interesBonificable,
                PagoMinimo = cuotaMinima,
                PagoContadoConInteres = totalConIntereses,
                FechaCreacion = DateTime.Now,
                Transacciones=transaccionesMesActual.OrderByDescending(d=>d.FechaTransaccion).ToList(),
            };

            return estadoCuentaDto;
        }

        public Task<EstadosCuentaDto> GetEstadodeCuentaOfThisMontByTarjeta(int? TarjetaId)
        {
            throw new NotImplementedException();
        }
    }
}
