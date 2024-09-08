using AutoMapper;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using DTO.DTO.Models;
using Microsoft.AspNetCore.Mvc;
using DTO.DTO.Models;
using Core.Infraestructure.DTO;
using Core.Infraestructure.Exceptions;
using DTO.DTO.Request;
using BCuentas.Helpers.Validators;

[ApiController]
[Route("api/[controller]")]

public class CuentasController : ControllerBase
{
   
    private readonly IMapper _mapper;
    private readonly IEstadoCuentaRepository _estadoCuentaRepository;

    private ILogger<CuentasController> _logger;
    public CuentasController(IEstadoCuentaRepository estadoCuentaRepository, IMapper mapper,ILogger<CuentasController> logger)
    {
         _estadoCuentaRepository = estadoCuentaRepository;
         _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Obtiene el estado de cuenta para una tarjeta en un rango de fechas específico.
    /// </summary>
    /// <remarks>
    /// Este endpoint permite obtener el estado de cuenta asociado a una tarjeta, especificando un rango de fechas.
    /// 
    /// <para>Header:</para>
    /// <para>Authorization: JWT Firebase Auth</para>
    /// 
    /// <para>En el campo 'TarjetaId', se debe proporcionar un ID válido de tarjeta.</para>
    /// <para>Las fechas deben estar en formato ISO 8601.</para>
    /// </remarks>
    /// <param name="request">Objeto que contiene la tarjeta, fecha de inicio y fecha de fin para la consulta.</param>
    /// <returns>Un objeto BaseResponse que contiene los datos del estado de cuenta o un mensaje de error si no se encuentra.</returns>
    /// <response code="200">Devuelve el estado de cuenta solicitado.</response>
    /// <response code="400">Devuelve un mensaje de error si los parámetros son inválidos.</response>
    /// <response code="404">Devuelve un mensaje de error si la tarjeta no se encuentra.</response>
    [HttpPost("GetEstadoCuenta")]
    public async Task<BaseResponse<EstadosCuentaDto>> GetEstadoCuenta([FromBody] EstadoCuentaRequestDto request)
    {
        try
        {
            _logger.LogInformation("Iniciando la obtención del estado de cuenta para la tarjeta {tarjetaId} Desde {fechaInicio} - hasta {fechaFin}", request.TarjetaId, request.FechaInicio, request.FechaFin);

            var estadoCuenta = await _estadoCuentaRepository.GetEstadodeCuentaByTarjetaAndRangeDates(request.TarjetaId, request.FechaInicio, request.FechaFin);

            _logger.LogInformation("Se obtuvo satisfactoriamente el estado de cuenta para la tarjeta {tarjetaId} estado de cuenta {@EstadoCuenta}", request.TarjetaId, estadoCuenta);
            return new BaseResponse<EstadosCuentaDto>(estadoCuenta);
        }
        catch (CreditCardNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return new BaseResponse<EstadosCuentaDto>() { Message = ex.Message, Status = false };
        }
        catch (ClientNotFoundException ex)
        {
            _logger.LogError(ex.Message);
            return new BaseResponse<EstadosCuentaDto>() { Message = ex.Message, Status = false };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new BaseResponse<EstadosCuentaDto>() { Message = ex.Message, Status = false };
        }
    }
}