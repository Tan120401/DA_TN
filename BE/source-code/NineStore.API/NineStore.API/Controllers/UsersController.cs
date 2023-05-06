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
using System.IO;
using NineStore.Common.Enum;
using NineStore.Common.Resource;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;

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

        /// <summary>
        /// Hàm đăng nhập
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Hàm upload ảnh
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        [HttpPost("upload-file")]
        public IActionResult CreateFile([FromForm] FileModel fileModel)
        {
            string path = Path.Combine(@"D:\DO_AN\FE\app\src\assets\img\user", fileModel.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                fileModel.File.CopyTo(stream);
            }
            return StatusCode(StatusCodes.Status200OK, 1);
        }

        /// <summary>
        /// Hàm trả tạo token
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private string CreateToken(UserRequest result)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.UserName),
                new Claim(ClaimTypes.Role, result.Role),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Hàm xử lý ảnh
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        protected override string CreatePathImg(UserRequest user)
        {
            string path = "";
            if (user.ImgName != null)
            {
                path = Path.Combine(@"", user.ImgName);
            }
            return path;
        }


        /// <summary>
        /// Hàm gửi email
        /// </summary>
        /// <param name="request"></param>
        private void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("theninestore68@gmail.com"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("theninestore68@gmail.com", "qrqrccjtesvpsdrh");
                client.Send(email);
                client.Disconnect(true);
            }
        }
        private string GenerateNewPassword()
        {
            string password = Guid.NewGuid().ToString("d").Substring(1, 8);
            return password;
        }
        [HttpPost("fogot-password")]
        public virtual IActionResult ForgotPassword(string userName)
        {
            try
            {
                string newPassword = GenerateNewPassword();
                var result = _userBL.ForgotPassword(userName, newPassword);
                //Xử lý kết quả trả về
                if (result.IsSuccess)
                {
                    EmailDto request = new EmailDto();
                    request.To = userName;
                    request.Subject = "The NineStore xin chào quý khách!";
                    request.Body = "Mật khẩu mới của quý khách là: "+ newPassword;
                    SendEmail(request);
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = Resource.SystemError,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }

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
