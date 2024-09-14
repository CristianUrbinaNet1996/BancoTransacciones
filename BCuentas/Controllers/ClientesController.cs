using AutoMapper;
using Core.Infraestructure.DTO;
using Core.Infraestructure.DTO.Models;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCuentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientesRepository _clienteRepo;


        private ILogger<EstadoCuentaController> _logger;
        public ClientesController(IClientesRepository clientesRepository, IMapper mapper, ILogger<EstadoCuentaController> logger)
        {
            _clienteRepo = clientesRepository;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        public async Task<BaseResponse<List<ClienteDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando obtencion de clientes");

            try
            {
                var result = await _clienteRepo.GetClientes();
                return new BaseResponse<List<ClienteDto>>(result);
            }
            catch (WrongCredentialsException ex)
            {

                _logger.LogError(ex.Message);
                return new BaseResponse<List<ClienteDto>>() { Message = ex.Message, Status = false };

            }

            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                return new BaseResponse<List<ClienteDto>>() { Message = ex.Message, Status = false };

            }

        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClientesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
