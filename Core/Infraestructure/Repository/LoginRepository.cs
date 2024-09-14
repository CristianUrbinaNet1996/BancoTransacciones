using AutoMapper;
using BCuentas.Utils;
using Core.Infraestructure.DTO.Models;
using Core.Infraestructure.Exceptions;
using Core.Infraestructure.Interfaces;
using Core.Infraestructure.Models;
using DTO.DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infraestructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private BcuentasContext _contex; 
        private IConfiguration _configuration;
        private ILogger<LoginRepository> _logger;
        private IMapper _mapper;
        public LoginRepository(BcuentasContext contex, IConfiguration configuration,IMapper mapper)
        {
            _contex = contex;
            _configuration = configuration; 
            _mapper = mapper;
        }
        public ClienteDto Login(LoginDto formulario)
        {
            try
            {
                var pass = _configuration["ENCRYPTBYPASSPHRASE"];
                var result = this._contex.sp_LoginResult.FromSqlInterpolated(
                    $"EXEC [dbo].[sp_login] @Usuario= {formulario.Email} ,@Contrasena = {formulario.Password} , @llave = {pass} ").ToList();

                if (result.Count() < 1)
                {
                    throw new WrongCredentialsException(CustomMessages.ErrorMessages.CredencialesIncorrectas);
                }
                return _mapper.Map<ClienteDto>(result.FirstOrDefault());
            }
            catch(WrongCredentialsException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error SP");
                return null;
            }
        }
    }
}
