using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.BL.RestDetailBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Resources;

namespace MISA.AMIS.API.Controllers
{
   
    public class RestDetailsController : BasesController<RestDetail>
    {
        #region Field

        private IRestDetailBL _RestDetailBL;

        #endregion

        #region Constructor
        public RestDetailsController(IRestDetailBL RestDetailBL) : base(RestDetailBL)
        {
            _RestDetailBL = RestDetailBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API lấy danh đơn lọc theo trang
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm</param>
        /// <param name="statusId">Từ khóa tìm kiếm theo trạng thái</param>
        /// <param name="departmentId">Từ khóa tìm kiếm theo phòng ban</param>
        /// <param name="filterParams">Đối tượng lọc nâng cao</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <returns>Danh sách đơn</returns
        /// Created by: NVTan (14/03/2023)
        [HttpPost("filter")]
        public IActionResult GetRestDetailsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] string? statusId,
            [FromQuery] string? depId,
            List<FilterParams>? filterParams,
            [FromQuery] int pageSize = 50,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {
                var result = new PagingResult();
                result = _RestDetailBL.GetRestDetailsByFilter(keyword, statusId, depId, filterParams, pageSize, pageNumber);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần xóa </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        [HttpDelete("restDetailIds")]
        public IActionResult DeleteRestDetailMultiple(List<Guid> restDetailIds)
        {
            try
            {
                int numberOfAffectedRows = _RestDetailBL.DeleteRestDetailMultiple(restDetailIds);
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, 1);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.NoData,
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
        /// Hàm cập nhật trạng thái nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần cập nhật trạng thái </param>
        /// <param name="statusId"> Id trạng thái cần cập nhật</param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        [HttpPut("updateRestDetail/{statusId}")]
        public IActionResult UpdateRestDetailMultiple(int statusId, List<Guid> restDetailIds)
        {
            try
            {
                int numberOfAffectedRows = _RestDetailBL.UpdateRestDetailMultiple(statusId, restDetailIds);
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, 1);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                    {
                        ErrorCode = Common.Enums.ErrorCode.NoData,
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
        /// Hàm xuất khẩu dữ liệu
        /// </summary>
        /// <param name="RestDetailIds">danh sách id đơn cần xuất khẩu</param>
        /// <returns>
        /// Nếu có id thì xuất khẩu theo danh sách id
        /// Không có id thì xuất khẩu tất cả
        /// </returns>
        /// Created By: NVTAN (09/03/2023)
        [HttpPost("export")]
        public async Task<IActionResult> ExportToExcel(CancellationToken cancellationToken, List<Guid>? listRestDetailId)
        {
            await Task.Yield();
            var stream = _RestDetailBL.ExportToExcel(listRestDetailId);
            string excelName = "Đơn_xin_nghỉ_Tất cả đơn vị.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
                errorCode = Common.Enums.ErrorCode.Exception,
                devMsg = ex.Message,
                userMsg = Resource.SystemError,
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorExp);
        } 

        #endregion
    }
}
