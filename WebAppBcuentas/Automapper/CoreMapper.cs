using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppBcuentas.Areas.EstadoCuentas.Models;
using WebAppBcuentas.Models.Dto;

namespace WebAppBcuentas.Automapper
{
    public class CoreMapper:Profile
    {
        public CoreMapper() { 

           CreateMap<EstadosCuentaDto,EstadoCuentaVM>().ReverseMap();

            CreateMap<ClienteDto, SelectListItem>()
                    .ForMember(d => d.Value, opt => opt.MapFrom(src => src.ClienteId.ToString()))
                    .ForMember(d => d.Text, opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido));

        }
    }
}
