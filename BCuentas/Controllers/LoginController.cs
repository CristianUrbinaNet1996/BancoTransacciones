using Core.Infraestructure.DTO;
using Core.Infraestructure.DTO.Models;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using DTO.DTO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BCuentas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private ILoginRepository _loginRepository;
        private ILogger<LoginController> _logger;

        public LoginController(ILoginRepository loginRepository, ILogger<LoginController> logger)
        {
            _loginRepository = loginRepository;
            _logger = logger;
        }

        [HttpPost]
        public BaseResponse<ClienteDto> Login (LoginDto login)
        {

            _logger.LogInformation("Iniciando validacion para login con las credenciales {@login}",login);

            try
            {
                var result = _loginRepository.Login (login);    
                return new BaseResponse<ClienteDto>(result);
            }
            catch (WrongCredentialsException ex) 
            {

                _logger.LogError(ex.Message);
                return new BaseResponse<ClienteDto>() { Message = ex.Message, Status = false };

            }

            catch (Exception ex) {

                _logger.LogError(ex.Message);
                return new BaseResponse<ClienteDto>() { Message = ex.Message ,Status=false};
            
            }


        }

    }
}
