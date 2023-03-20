using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PatientWebApi.Interfaces;
using PatientWebApi.Models;
using PatientWebApi.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PatientWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Users user = new Users();
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _userService;

        public AuthController(IConfiguration configuration, IAuthenticate userService) { 
            _configuration = configuration;
            _userService = userService;
        }

       

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(UserDto request) {

        //     CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passworkdSalt);
        //    user.Username = request.Username;
        //    user.PasswordSalt = passworkdSalt;
        //    user.PasswordHash = passwordHash;

        //    return Ok(user);
       

        //}

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Credentialinfo credentialinfo) {


            var result = await _userService.AuthenticateUser(credentialinfo.username, credentialinfo.password);
            if (result.IsSuccess)
            {

                string token = CreateUserToken(result.user);
                var refreshToken = GetRefreshToken();
                SetRefreshToken(refreshToken);
               // result.user.RefreshToken = c;
               return Ok(token);

            }
            return NotFound();

       
          
        }
        private RefreshToken GetRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
              };
            return refreshToken;

        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly= true,
                Expires= newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken",newRefreshToken.Token, cookieOptions);
            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires= newRefreshToken.Expires;

        }

        

        private string CreateUserToken(Users user)
        {
            List<Claim> claims = new List<Claim> {

             new Claim(ClaimTypes.Name, user.UserName),
             new Claim(ClaimTypes.Role, "Admin")
            };
            //new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
          //  (System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims : claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        
        }

        [HttpPost("refesh-Token")]
        public async Task<ActionResult<string>> RefeshToken()
        {

            var refreshToken = Request.Cookies["refreshToken"];
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refesh Token");
            }
            else if (user.TokenExpires < DateTime.Now) {

                return Unauthorized("Token Expired");

            }

            var token = CreateUserToken(user);
            var newRefreshToken = GetRefreshToken();
            SetRefreshToken(newRefreshToken);
            return Ok(token);


        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
