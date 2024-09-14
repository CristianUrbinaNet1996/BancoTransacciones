using WebAppBcuentas.Models.Dto;

namespace WebAppBcuentas.Areas.Transacciones.Interface
{
    public interface ITransaccion
    {
        Task<List<TransaccionesDTO>> TransaccionesByidTarjeta(int id);

        Task<TransaccionesDTO> AddTransaccion(TransaccionesDTO transaccionDTO);
    }
}
