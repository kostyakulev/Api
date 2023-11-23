using Api.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers.v2
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class AuthController : ControllerBase
    {
        public static Auth auth = new Auth();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Method for registering a new user.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /register
        ///     {
        ///        "username": "exampleUser",
        ///        "password": "examplePassword"
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Data for registering a new user.</param>
        /// <returns>Returns information about the registered user.</returns>
        /// <response code="200">Successful registration. Returns information about the registered user.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(Auth), 200)] // Specifies the data type for a successful response
        public async Task<ActionResult<Auth>> Register(AuthDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            auth.Username = request.Username;
            auth.PasswordHash = passwordHash;
            auth.PasswordSalt = passwordSalt;

            return Ok(auth);

        }
        /// <summary>
        /// Method for authenticating a user.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     POST /login
        ///     {
        ///        "username": "exampleUser",
        ///        "password": "examplePassword"
        ///     }
        /// 
        /// </remarks>
        /// <param name="request">Data for authentication.</param>
        /// <returns>Returns an authorization token in case of successful authentication.</returns>
        /// <response code="200">Successful authentication. Returns an authorization token.</response>
        /// <response code="400">Incorrect authentication data (e.g., incorrect username or password).</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)] // Specifies the data type for a successful response
        [ProducesResponseType(typeof(string), 400)] // Specifies the data type for an error response
        public async Task<ActionResult<Auth>> Login(AuthDto request)
        {

            if (auth.Username != request.Username)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, auth.PasswordHash, auth.PasswordSalt))
            {

                return BadRequest("Wrong password.");
            }

            string token = CreateToken(auth);

            return Ok("bearer" + " " + token);

        }
        
        private string CreateToken(Auth auth)
        {
            List<Claim> claims = new List<Claim> {
              new Claim(ClaimTypes.Name, auth.Username),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
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
