using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.OrderDetailBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    
    public class OrderDetailsController : BasesController<OrderDetail>
    {
        #region Field

        private IOrderDetailBL _orderDetailBL;

        #endregion
        public OrderDetailsController(IOrderDetailBL orderDetailBL) : base(orderDetailBL)
        {
            _orderDetailBL = orderDetailBL;
        }
    }
}
