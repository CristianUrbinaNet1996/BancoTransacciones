using AutoMapper;
using BCuentas.Utils;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using DTO.DTO.Enums;
using DTO.DTO.Models;
using DTO.DTO.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Repository
{
    public class TransaccionesRepository:ITransaccionesRepository
    {
        private BcuentasContext _context;
        private IMapper _mapper;
        private ILogger<TransaccionesRepository> _logger;   
        public TransaccionesRepository(BcuentasContext context, IMapper mapper,ILogger<TransaccionesRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TransaccionesDTO> CreateTransaccion(CreateTransaccionRequestDto transaccion)
        {
            var tarjeta = await _context.TarjetasCreditos.FindAsync(transaccion.TarjetaId);
            Transaccione TransactionModel = new Transaccione(); 
            if (tarjeta == null)
            {
                throw new CreditCardNotFoundException(string.Format(CustomMessages.ErrorMessages.TarjetaNoEncontrada, transaccion.TarjetaId), transaccion.TarjetaId);
            }

            try
            {


                if ((TipoTransaccion.TipoTransacción)transaccion.TipoTransaccion == TipoTransaccion.TipoTransacción.Compra)
                {
                    if (tarjeta.SaldoDisponible >= transaccion.Monto)
                    {
                         TransactionModel = _mapper.Map<Transaccione>(transaccion);
                        _context.Transacciones.Add(TransactionModel);

                        tarjeta.SaldoActual += transaccion.Monto;

                        _context.Entry(tarjeta).State = EntityState.Modified;

                        _context.SaveChanges();


                    }
                    else
                    {
                        throw new InsufficientFundsException(string.Format(CustomMessages.ErrorMessages.FondosInsuficientes, tarjeta.TarjetaId));
                    }


                }

             




                return _mapper.Map<TransaccionesDTO>(TransactionModel);
            }
            catch (Exception ex) {

                _logger.LogError( string.Format(CustomMessages.ErrorMessages.FalloAlguardarEnDb,nameof(Transaccione)));
                throw;
            }
          
        


        }

        public async Task<List<TransaccionesDTO>> GetTransaccionesByIdTarjeta(int tarjetaId)
        {
            var transacciones = await _context.Transacciones
                .Include(d => d.Tarjeta)
                   .ThenInclude(r => r.Cliente)
                   .Where(d => d.TarjetaId == tarjetaId).ToListAsync();

            return _mapper.Map<List<TransaccionesDTO>>(transacciones);
        }
    }
}
