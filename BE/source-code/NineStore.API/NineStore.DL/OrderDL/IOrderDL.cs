using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.OrderDL
{
    public interface IOrderDL: IBaseDL<Order>
    {
        public List<OrderDetailService> GetOrderDetailService(Guid? orderId);
        public List<OrderDetailService> GetAllOrderDetailService();
        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword);
        public dynamic GetRecordByFilterAndUserId(int pageSize, int pageNumber, string? keyword, Guid? userId);

    }
}
