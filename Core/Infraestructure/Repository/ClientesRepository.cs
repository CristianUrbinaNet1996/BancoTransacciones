using AutoMapper;
using Core.Infraestructure.DTO.Models;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Repository
{
    public class ClientesRepository : IClientesRepository
    {
        private BcuentasContext _context;
        private IMapper _mapper;
        public ClientesRepository(BcuentasContext context, IMapper mapper) {
        _context = context;
        _mapper = mapper;
        }
        public async Task<ClienteDto> GetClienteById(int id)
        {
          var cliente = await _context.Clientes.FindAsync(id);
            return _mapper.Map<ClienteDto>(cliente);    
        }
        public async Task<List<ClienteDto>> GetClientes()
        {
            var cliente = await _context.Clientes.ToListAsync();
            return _mapper.Map<List<ClienteDto>>(cliente);
        }
    }
}
