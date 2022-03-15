using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnCourse.Data;
using OnCourse.Enums;
using OnCourse.Settings;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OnCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly OnCourseContext _context;

        public AuthenticatorController(IOptions<JwtConfiguration> jwtConfiguration, OnCourseContext context)
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _context = context;
        }

        [HttpGet]
        public ActionResult ObterToken(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user == null)
            {
                return NotFound();
            }

            var result = new
            {
                token = GerarToken(user.Role)
            };

            return Ok(result);
        }

        private string GerarToken(EnUserRole role)
        {
            var claims = new List<Claim>();

            if (role == EnUserRole.MANAGER)
                claims.Add(new Claim(ClaimTypes.Role, "Manager"));

            if (role == EnUserRole.SECRETARY)
                claims.Add(new Claim(ClaimTypes.Role, "Secretary"));

            var handler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfiguration.ApiKey)),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Audience = _jwtConfiguration.Audience,
                Issuer = _jwtConfiguration.Issuer,
                Subject = new ClaimsIdentity(claims)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }
    }
}
