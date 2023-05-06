using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.OrderBL
{
    public interface IOrderBL : IBaseBL<Order>
    {
        public List<OrderDetailService> GetOrderDetailServices(Guid? orderId);

        public List<OrderDetailService> GetAllOrderDetailService();

        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyWord);
        public dynamic GetRecordByFilterAndUserId(int pageSize, int pageNumber, string? keyword, Guid? userId);


    }
}
