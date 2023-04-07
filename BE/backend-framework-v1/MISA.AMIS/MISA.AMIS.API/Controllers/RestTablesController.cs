using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.BL.RestTableBL;
using MISA.AMIS.Common.Constrants;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Resources;
using MySqlConnector;
using System.Data;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RestTablesController : ControllerBase
    {
        #region Field

        private IRestTableBL _restTableBL;

        #endregion

        #region Constructor
        public RestTablesController(IRestTableBL restTableBL)
        {
            _restTableBL = restTableBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách cột
        /// </summary>
        /// <returns>Danh sách cột</returns>
        [HttpGet]
        public IActionResult GetAllRestTable()
        {
            try
            {
                List<RestTable> result = _restTableBL.GetAllRestTable();
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm xóa danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteRestTable()
        {

            try
            {
                int numberAffectedRows = _restTableBL.DeleteRestTable();
                if (numberAffectedRows > 0)
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
        /// Hàm chèn thông tin cột 
        /// </summary>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// <summary>
        [HttpPost]
        public IActionResult InsertMupltyRestTable(List<RestTable> restTables)
        {
            try
            {
                int numberAffectedRows = _restTableBL.InsertMupltyRestTable(restTables);
                if (numberAffectedRows > 0)
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
