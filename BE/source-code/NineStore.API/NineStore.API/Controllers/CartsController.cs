using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.CartBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.Common.Enum;
using NineStore.Common.Resource;

namespace NineStore.API.Controllers
{

    public class CartsController : BasesController<Cart>
    {
        #region Field

        private ICartBL _cartBL;

        #endregion
        public CartsController(ICartBL cartBL) : base(cartBL)
        {
            _cartBL = cartBL;
        }

        #region Method
        
        /// <summary>
        /// Hàm lấy thông tin cart thông qua user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("get-cart/{userId}")]
        public IActionResult GetCartByUserId(Guid? userId)
        {
            try
            {
                List<CartResponse> results = _cartBL.GetCartByUser(userId);
                if (results == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, results);
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
                errorCode = ErrorCode.Exception,
                devMsg = ex.Message,
                userMsg = Resource.SystemError
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorExp);
        }
        #endregion
    }
}
