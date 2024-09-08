using AutoMapper;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
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
        private IParametrosConfiguracion _parametrosRepository;
        private IMapper _mapper;
        public EstadoCuentaRepository(BcuentasContext context, IClientesRepository clientesRepository, IParametrosConfiguracion parametrosConfiguracion, ITarjetaRepository tarjetaRepository, IMapper mapper, ITransaccionesRepository transaccionesRepository)
        {

            _context = context;
            _clientesRepository = clientesRepository;
            _tarjetaRepository = tarjetaRepository;
            _mapper = mapper;
            _transaccionesRepository = transaccionesRepository;
            _parametrosRepository = parametrosConfiguracion;
        }
        
        public async Task<EstadosCuentaDto> GetEstadodeCuentaByTarjetaAndRangeDates(int? TarjetaId,DateTime fechaInicio,DateTime fechaFin)
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

            // 3. Obtener las transacciones en el rango de fechas (mes actual)
            var transacciones = await _transaccionesRepository.GetTransaccionesByIdTarjeta(TarjetaId.Value);
            var transaccionesMesActual = transacciones
                .Where(t => t.FechaTransaccion >= fechaInicio && t.FechaTransaccion <= fechaFin && t.TarjetaId == TarjetaId)
                .ToList();

            // 4. Calcular los totales
            var saldoTotal = transaccionesMesActual.Sum(t => t.Monto);
            var totalComprasMesActual = transaccionesMesActual.Sum(t => t.TipoTransaccion == "Compra" ? t.Monto : 0);

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
                Cliente = $"{cliente.Nombre} {cliente.Apellido}",
                Mes = fechaInicio.Month,
                Anio = fechaInicio.Year,
                SaldoMesAnterior = 0, // Aquí puedes ajustar para obtener el saldo del mes anterior si lo necesitas
                SaldoMesActual = saldoTotal,
                InteresBonificable = interesBonificable,
                PagoMinimo = cuotaMinima,
                PagoContadoConInteres = totalConIntereses,
                FechaCreacion = DateTime.Now
            };

            return estadoCuentaDto;
        }
    }
}
