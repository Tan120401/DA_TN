using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Resources;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: NVTan (09/02/2023)
        [HttpGet]
        public IActionResult GetAllRecord()
        {
            try
            {
                List<T> listRecords;
                listRecords = _baseBL.GetAllRecord();
                // Xử lý kết quả trả về
                // Thành công
                if (listRecords != null)
                {
                    return StatusCode(StatusCodes.Status200OK, listRecords);
                }
                // Thất bại
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        ErrorCode = Common.Enums.ErrorCode.NoData,
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
        /// Tìm bản ghi theo ID
        /// </summary>
        /// <param name="recordId">ID bản ghi cần tìm kiếm</param>
        /// <returns>Bản ghi cần tìm kiếm</returns>
        /// Created by: NVTan (16/01/2023)
        [HttpGet("{recordId}")]
        public IActionResult GetRecordById(Guid recordId)
        {
            try
            {
                dynamic record;
                record = _baseBL.GetRecordById(recordId);
                if (record == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created By: NVTAN (09/02/2023)

        [HttpPost]
        public virtual IActionResult InsertRecord(T record)
        {
            try
            {
                var result = _baseBL.InsertRecord(record);
                //Xử lý kết quả trả về
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (!result.IsSuccess && result.ErrorCode == Common.Enums.ErrorCode.IsValidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        DevMsg = Resource.SystemError,
                        UserMsg = result.Message,
                        MoreInfo = result.Data,
                        TraceId = HttpContext.TraceIdentifier
                    });
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
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi</param>
        /// <param name="record">bản ghi muốn sửa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        [HttpPut("{recordId}")]
        public virtual IActionResult UpdateRecord(Guid recordId, T record)
        {
            try 
            {
                var result = _baseBL.UpdateRecord(recordId, record);
                if (result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status200OK, 1);
                }
                else if (!result.IsSuccess && result.ErrorCode == Common.Enums.ErrorCode.IsValidData)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = result.ErrorCode,
                        MoreInfo = result.Data,
                        DevMsg = result.Message,
                        TraceId = HttpContext.TraceIdentifier
                    });
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
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="employeeId">ID bản ghi</param>
        /// <returns>Danh sách bản ghi mới</returns>
        /// Created by: NVTan (30/01/2023)
        [HttpDelete("{recordId}")]
        public virtual IActionResult DeleteRecord(Guid recordId)
        {

            try
            {
                int numberOfAffectedRows;
                numberOfAffectedRows = _baseBL.DeleteRecord(recordId);
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
                userMsg = Resource.SystemError
            };
            return StatusCode(StatusCodes.Status500InternalServerError, errorExp);
        } 

        #endregion
    }
}
