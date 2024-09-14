using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppBcuentas.Models.Dto;


namespace WebAppBcuentas.Controllers
{
    public class LoginController : Controller
    {
      
        private readonly IConfiguration _configuration;

        public LoginController( IConfiguration configuration)
        {
           
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var cliente = new ClienteDto();

            if (cliente == null)
            {
                ModelState.AddModelError("", "Credenciales incorrectas.");
                return View(loginDto);
            }

            // Crear los claims (puedes agregar más si lo necesitas)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, cliente.ClienteId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, cliente.Email),
                new Claim(ClaimTypes.Name, cliente.Nombre + " " + cliente.Apellido)
            };

            // Generar el token JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Guardar el token en una cookie o en el session storage
            HttpContext.Response.Cookies.Append("X-Auth-Token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Asegúrate de tener HTTPS habilitado
                SameSite = SameSiteMode.Strict
            });

            return RedirectToAction("Index", "Home"); // Redirige a la página de inicio u otra página
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Eliminar el token de la cookie
            HttpContext.Response.Cookies.Delete("X-Auth-Token");
            return RedirectToAction("Login");
        }
    }
}