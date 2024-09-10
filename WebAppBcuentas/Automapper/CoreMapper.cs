using AutoMapper;
using WebAppBcuentas.Areas.EstadoCuentas.Models;
using WebAppBcuentas.Models.Dto;

namespace WebAppBcuentas.Automapper
{
    public class CoreMapper:Profile
    {
        public CoreMapper() { 

           CreateMap<EstadosCuentaDto,EstadoCuentaVM>().ReverseMap();  
        
        
        }
    }
}
