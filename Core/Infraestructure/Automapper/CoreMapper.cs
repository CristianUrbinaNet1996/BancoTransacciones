using AutoMapper;
using Core.Infraestructure.DTO.Models;
using Core.Infraestructure.Models;
using DTO.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Automapper
{
    public class CoreMapper : Profile 
    {

        public CoreMapper() {


            CreateMap<Cliente, ClienteDto>();

            CreateMap<TarjetasCredito, TarjetasCreditoDto>()
                .ForMember(d => d.Cliente, opt => opt.MapFrom(src => string.Concat(src.Cliente.Nombre, " ", src.Cliente.Apellido)));

            CreateMap<EstadosCuentum, EstadosCuentaDto>()
            .ForMember(d => d.Cliente, opt => opt.MapFrom(src => string.Concat(src.Tarjeta.Cliente.Nombre, " ", src.Tarjeta.Cliente.Apellido)));

            CreateMap<Transaccione, TransaccionesDTO>()
                    .ForMember(d => d.Cliente, opt => opt.MapFrom(src => string.Concat(src.Tarjeta.Cliente.Nombre, " ", src.Tarjeta.Cliente.Apellido)));
                   
            CreateMap<ParametrosConfiguracion, ParametrosConfiguracionDto>();

        }
    }
}
