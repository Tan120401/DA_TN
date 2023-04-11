using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.UserBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.DL.UserDL;
using NineStore.BL;
using NineStore.BL.BaseBL;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace NineStore.API.Controllers
{
    public class UsersController : BasesController<UserRequest>
    {
        #region Field

        private IUserBL _userBL;
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration, IUserBL userBL) : base(userBL)
        {
            _userBL = userBL;
            _configuration = configuration;
        }

        #endregion

        #region Constructor



        #endregion

        #region Method

        /*[HttpPost("Login")]

        public IActionResult LoginResult([FromBody]  UserRequest user)
        {
            try
            {
                dynamic result = _userBL.LoginResult(user);
                if (result != null)
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }*/

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(UserRequest user)
        {
            try
            {
                List<UserRequest> result = _userBL.LoginResult(user);
                if (result.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, 0);
                }
                string token = CreateToken(result[0]);
                UserResponse userResponse = new UserResponse(result[0], token);
                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string CreateToken(UserRequest result)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Xử lý ngoại lệ trả về lỗi
        /// </summary>
        /// <param name="ex">ngoại lệ</param>
        /// <returns>devMsg và userMsg</returns>
        /// Created by: NVTan (04/02/2023)
        private IActionResult HandleException(Exception ex)
        {
            var errorExp = new
            {
                errorCode = 0,
                devMsg = "error",
                userMsg = "error",
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorExp);
        }

        #endregion
    }
}
