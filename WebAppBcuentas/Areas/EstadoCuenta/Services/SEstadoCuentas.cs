using AutoMapper;
using WebAppBcuentas.Areas.EstadoCuentas.Interfaces;
using WebAppBcuentas.Areas.EstadoCuentas.Models;
using WebAppBcuentas.Models;
using WebAppBcuentas.Models.Dto;
using WebAppBcuentas.Services;

namespace WebAppBcuentas.Areas.EstadoCuentas.Services
{
    public class SEstadoCuentas : IEstadoCuenta
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public SEstadoCuentas(IHttpClientFactory httpClientFactory,IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }
        public async Task<EstadoCuentaVM> GetEstadoCuentaByTarjeta(int Id)
        {
            var datos = new EstadosCuentaDto();

            var resultado = await _httpClientFactory.ExcuteAPI<BaseResponse>("API", $"EstadoCuenta/GetEstadoCuentaOfThisMonth/{Id}", ApiService.GET, null);
            datos = resultado.ToObjet<EstadosCuentaDto>();

            return _mapper.Map<EstadoCuentaVM>(datos);

        }
    }
}
