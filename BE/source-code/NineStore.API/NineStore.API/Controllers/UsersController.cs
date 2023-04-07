using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.UserBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.DL.UserDL;
using NineStore.BL;
using NineStore.BL.BaseBL;

namespace NineStore.API.Controllers
{
    public class UsersController : BasesController<User>
    {
        #region Field

        private IUserBL _userBL;

        public UsersController(IUserBL userBL):base(userBL)
        {
            _userBL = userBL;
        }

        #endregion

        #region Constructor



        #endregion

        #region Method

        [HttpPost("Login")]

        public IActionResult LoginResult([FromBody]  User user)
        {
            try
            {
                int result = _userBL.LoginResult(user);
                if (result == 1)
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
