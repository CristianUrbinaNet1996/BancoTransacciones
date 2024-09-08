using AutoMapper;
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
    public class TarjetasRepository : ITarjetaRepository
    {
        private BcuentasContext _context;
        private IMapper _mapper;

        public TarjetasRepository(BcuentasContext context,IMapper mapper) { 
        _context=context;
            _mapper=mapper;
        
        }
        public async Task<TarjetasCreditoDto> Get(int id)
        {
           var tarjeta = await _context.TarjetasCreditos.FindAsync(id);
            return _mapper.Map<TarjetasCreditoDto>(tarjeta);
        }
    }
}
