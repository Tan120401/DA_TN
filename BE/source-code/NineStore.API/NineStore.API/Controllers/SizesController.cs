using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.SizeBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.Common.Enum;
using NineStore.Common.Resource;

namespace NineStore.API.Controllers
{
    
    public class SizesController : BasesController<Size>
    {
        #region Field

        private ISizeBL _sizeBL;

        #endregion
        public SizesController(ISizeBL sizeBL) : base(sizeBL)
        {
            _sizeBL = sizeBL;

            
        }

        /// <summary>
        /// Hàm chèn nhiều kích thước thuộc 1 sản phẩm
        /// </summary>
        /// <param name="Sizes"></param>
        /// <returns></returns>
        [HttpPost("insert-mulpty")]
        public IActionResult InsertMupltySize(List<Size> Sizes)
        {
            try
            {
                int numberAffectedRows = _sizeBL.InsertMupltySize(Sizes);
                if (numberAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, 1);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = ErrorCode.NoData,
                        DevMsg = Resource.SystemError,
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
        /// Hàm lấy thông tin kích thước thông qua id sản phẩm
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("get-by-productid/{productId}")]
        public IActionResult GetSizeByProductId(Guid productId)
        {
            try
            {
                List<Size> results = _sizeBL.GetSizeByProductId(productId);
                if (results.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, results);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = ErrorCode.NoData,
                        DevMsg = Resource.SystemError,
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
                errorCode = ErrorCode.Exception,
                devMsg = ex.Message,
                userMsg = Resource.SystemError
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorExp);
        }
    }
}
