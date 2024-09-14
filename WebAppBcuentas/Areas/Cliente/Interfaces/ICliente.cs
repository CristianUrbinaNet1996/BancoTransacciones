using WebAppBcuentas.Models.Dto;

namespace WebAppBcuentas.Areas.Cliente.Interfaces
{
    public interface ICliente
    {

        Task<List<ClienteDto>> GetAll();
    }
}
