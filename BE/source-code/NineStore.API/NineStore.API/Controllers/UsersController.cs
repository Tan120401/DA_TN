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

        /// <summary>
        /// Khai báo hàm random
        /// </summary>
        private static readonly Random Random = new Random();
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericChars = "0123456789";
        private const string SpecialChars = "#$%^&*()_+";
        /// <summary>
        /// Hàm sinh mật khẩu mới
        /// </summary>
        /// <returns></returns>
        private string GenerateNewPassword()
        {
            var password = new char[8];

            // Add one lowercase character
            password[Random.Next(0, password.Length)] = LowercaseChars[Random.Next(0, LowercaseChars.Length)];

            // Add one uppercase character
            password[Random.Next(0, password.Length)] = UppercaseChars[Random.Next(0, UppercaseChars.Length)];

            // Add one numeric character
            password[Random.Next(0, password.Length)] = NumericChars[Random.Next(0, NumericChars.Length)];

            // Add one special character
            password[Random.Next(0, password.Length)] = SpecialChars[Random.Next(0, SpecialChars.Length)];

            // Fill in the remaining characters with random characters from all the character sets
            for (var i = 0; i < password.Length; i++)
            {
                if (password[i] == default(char))
                {
                    var characterSets = new[] { LowercaseChars, UppercaseChars, NumericChars, SpecialChars };
                    var randomCharSet = characterSets[Random.Next(0, characterSets.Length)];
                    password[i] = randomCharSet[Random.Next(0, randomCharSet.Length)];
                }
            }

            return new string(password);
        }

        /// <summary>
        /// Hàm quên mật khẩu
        /// </summary>
        /// <param name="userName">Tên đăng nhập</param>
        /// <returns></returns>
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
                    request.Body = "Mật khẩu mới của quý khách là: " + newPassword;
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
