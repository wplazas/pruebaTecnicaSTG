using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using pruebaTecnicaSTG.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace pruebaTecnicaSTG.Controllers
{
    [ApiController]
    [Route("user")]
    public class LoginController:ControllerBase
    {
        public IConfiguration _conf;

        public LoginController(IConfiguration conf)
        {
            _conf = conf;
        }
        [HttpPost]
        [Route("login")]
        public dynamic sessionInit([FromBody] Object odata)
        {
            var oinit = JsonConvert.DeserializeObject<dynamic>(odata.ToString());
            string user = oinit.username.ToString();
            string password = oinit.password.ToString();
            User usr = new User();
             usr = usr.DB().Where(x => x.username == user && x.password == password).FirstOrDefault();
            var jwt = _conf.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("username",usr.username.ToString()),
                new Claim("password",usr.password.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var sessionInit = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _conf["Jwt:Issuer"],
                _conf["Jwt:Audience"],
                claims,
                signingCredentials: sessionInit
                );

            return new
            {
                success=true,
                message="Ok cheers",
                result= new JwtSecurityTokenHandler().WriteToken(token)

            };

        }

    }
}
