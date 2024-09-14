using AutoMapper;
using WebAppBcuentas.Areas.Cliente.Interfaces;
using WebAppBcuentas.Models;
using WebAppBcuentas.Models.Dto;
using WebAppBcuentas.Services;

namespace WebAppBcuentas.Areas.Cliente.Services
{
    public class SCliente : ICliente
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public SCliente(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;

        }

        public async Task<List<ClienteDto>> GetAll()
        {
            var datos = new List<ClienteDto>();

            var resultado = await _httpClientFactory.ExcuteAPI<BaseResponse>("API", $"Clientes/GetAll", ApiService.GET, null);
            datos = resultado.ToObjet<List<ClienteDto>>();


            return datos;

        }
    }
}
