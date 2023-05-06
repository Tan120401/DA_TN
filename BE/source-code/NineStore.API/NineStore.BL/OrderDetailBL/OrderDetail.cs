using NineStore.BL.BaseBL;
using NineStore.BL.ProductBL;
using NineStore.Common.Entities;
using NineStore.DL.OrderDetailDL;
using NineStore.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.OrderDetailBL
{
    
    public class OrderDetailBL : BaseBL<OrderDetail>, IOrderDetailBL
    {
        #region Field

        private IOrderDetailDL _orderDetailDL;

        #endregion
        public OrderDetailBL(IOrderDetailDL orderDetailDL) : base(orderDetailDL)
        {
            _orderDetailDL = orderDetailDL;
        }

    }
}
