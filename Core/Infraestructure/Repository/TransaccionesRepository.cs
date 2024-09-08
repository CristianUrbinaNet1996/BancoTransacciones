using AutoMapper;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using DTO.DTO.Models;
using Microsoft.EntityFrameworkCore;
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
        public TransaccionesRepository(BcuentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
