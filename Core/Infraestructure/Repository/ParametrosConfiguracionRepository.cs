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
    public class ParametrosConfiguracionRepository:IParametrosConfiguracionRepository
    {
        private BcuentasContext _context;
        private IMapper _mapper;
        public ParametrosConfiguracionRepository(BcuentasContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParametrosConfiguracionDto> GetById(int id)
        {
            var parametro =await _context.ParametrosConfiguracions.FindAsync(id);

            return _mapper.Map<ParametrosConfiguracionDto>(parametro);
        }
    }
}
