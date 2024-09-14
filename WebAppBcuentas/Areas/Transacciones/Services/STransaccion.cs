using AutoMapper;
using WebAppBcuentas.Areas.Transacciones.Interface;
using WebAppBcuentas.Models;
using WebAppBcuentas.Models.Dto;
using WebAppBcuentas.Services;

namespace WebAppBcuentas.Areas.Transacciones.Services
{
    public class STransaccion : ITransaccion
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public STransaccion(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
           
        }

        public async Task<TransaccionesDTO> AddTransaccion(TransaccionesDTO transaccionDTO)
        {
           

            var resultado = await _httpClientFactory.ExcuteAPI<BaseResponse>("API", $"Transacciones/CreateTransaccion", ApiService.POST, transaccionDTO);
         

            return transaccionDTO;
        }

        public async  Task<List<TransaccionesDTO>> TransaccionesByidTarjeta(int id) {

        var datos = new List<TransaccionesDTO>();

        var resultado = await _httpClientFactory.ExcuteAPI<BaseResponse>("API", $"Transacciones/GetTransactionsByIdTarjeta/{id}", ApiService.GET, null);
        datos = resultado.ToObjet<List<TransaccionesDTO>>();

            return datos;
        
        }
    }
}
