using Core.Infraestructure.DTO;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using DTO.DTO.Models;
using DTO.DTO.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCuentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {

        private ITransaccionesRepository _transactions;
        private ILogger<TransaccionesController> _logger;
        public TransaccionesController (ITransaccionesRepository transactions,ILogger<TransaccionesController> logger)
        {
            _logger = logger;
            _transactions = transactions;
        }
     
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Obtiene las transacciones de la tarjeta.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite obtener las transacciones de  una tarjeta a partir de su id.
        /// 
        /// <para>Header:</para>
        /// <para>Authorization: JWT Firebase Auth</para>
        /// 
        /// <para>En el campo 'id', se debe proporcionar un ID válido de tarjeta.</para>
        ///
        /// </remarks>
        /// <param name="id">id de la tarjeta  para la consulta.</param>
        /// <returns>Un objeto BaseResponse que contiene los datos del estado de cuenta o un mensaje de error si no se encuentra.</returns>
        /// <response code="200">Devuelve las transacciones de la tarjeta solicitado.</response>
        /// <response code="400">Devuelve un mensaje de error si los parámetros son inválidos.</response>
        /// <response code="404">Devuelve un mensaje de error si la tarjeta no se encuentra.</response>
        [HttpGet("GetTransactionsByIdTarjeta/{id}")]
        public async Task<BaseResponse<List<TransaccionesDTO>>> Get(int id)
        {
            try
            {
                _logger.LogInformation("Iniciando con la obtencion de las transacciones para la tarjeta {id}",id);
                var result = await _transactions.GetTransaccionesByIdTarjeta(id);

                return new BaseResponse<List<TransaccionesDTO>>(result);
            
            }
            catch (CreditCardNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new BaseResponse<List<TransaccionesDTO>>() { Message= ex.Message, Status = false };  
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return new BaseResponse<List<TransaccionesDTO>>() { Message = ex.Message, Status = false };
            }
        }


        /// <summary>
        /// Creacion de transacciones de cualquier tipo.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite crear las transacciones de una tarjeta.
        /// 
        /// <para>Header:</para>
        /// <para>Authorization: JWT Firebase Auth</para>
        /// 
        ///
        /// </remarks>
        /// <param name="transacciones">Contiene el modelo request para crear la transaccion</param>
        /// <returns>Un objeto BaseResponse que contiene los datos de la transaccion que se acaba de crear</returns>
        /// <response code="200">Devuelve la transacción creada.</response>
        /// <response code="400">Devuelve un mensaje de error si los parámetros son inválidos.</response>
        /// <response code="404">Devuelve un mensaje de error si la tarjeta no se encuentra.</response>
        [HttpPost("CreateTransaccion")]
        public async Task<BaseResponse<TransaccionesDTO>> Post([FromBody] CreateTransaccionRequestDto transacciones)
        {
            try
            {
                _logger.LogInformation("Iniciando con la obtencion de las transacciones para la tarjeta {id}", transacciones.TarjetaId);
                var result = await _transactions.CreateTransaccion(transacciones);

                return new BaseResponse<TransaccionesDTO>(result);

            }
            catch (CreditCardNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new BaseResponse<TransaccionesDTO>() { Message = ex.Message,Status=false };
            }

            catch(InsufficientFundsException ex)
            {
                _logger.LogError(ex.Message);
                return new BaseResponse<TransaccionesDTO>() { Message = ex.Message, Status = false };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BaseResponse<TransaccionesDTO>() { Message = ex.Message,Status=false };
            }
        }

        // PUT api/<TransaccionesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransaccionesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
