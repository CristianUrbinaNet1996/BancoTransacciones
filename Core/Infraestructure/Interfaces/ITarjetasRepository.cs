﻿using DTO.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Interfaces
{
    public interface  ITarjetaRepository
    {
        Task<TarjetasCreditoDto> Get(int id);
    }
}
