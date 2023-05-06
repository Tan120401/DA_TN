using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.OrderBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.Common.Enum;
using NineStore.Common.Resource;

namespace NineStore.API.Controllers
{
    
    public class OrdersController : BasesController<Order>
    {
        #region Field

        private IOrderBL _orderBL;

        #endregion
        #region Constructor
        public OrdersController(IOrderBL orderBL) : base(orderBL)
        {
            _orderBL = orderBL;
        }
        #endregion

        #region Method

        [HttpGet("get-orderdetail-service/{orderId}")]
        public IActionResult GetOrderDetailServices(Guid? orderId)
        {
            List<OrderDetailService> results = _orderBL.GetOrderDetailServices(orderId);
            if (results != null)
            {
                return StatusCode(StatusCodes.Status201Created, results);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("get-all-orderservice")]
        public IActionResult GetAllOrderServices()
        {
            List<OrderDetailService> results = _orderBL.GetAllOrderDetailService();
            if (results != null)
            {
                return StatusCode(StatusCodes.Status201Created, results);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("filter-by-userId")]
        public IActionResult GetRecordByFilterAndUserId(
           [FromQuery] string? keyWord,
           Guid? userId, 
           [FromQuery] int pageSize = 10,
           [FromQuery] int pageNumber = 1
           )
        {
            try
            {
                dynamic records = _orderBL.GetRecordByFilterAndUserId(pageSize, pageNumber, keyWord,userId);

                if (records.Data.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, new ErrorResult
                    {
                        ErrorCode = ErrorCode.NoData,
                        DevMsg = Resource.ErrorToDL,
                        UserMsg = Resource.ErrorToDL,
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ex.Message,
                    UserMsg = Resource.SystemError,
                });
            }
        }
        #endregion
    }
}
