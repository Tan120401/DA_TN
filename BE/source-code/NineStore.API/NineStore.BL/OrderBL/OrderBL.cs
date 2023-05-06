using MailKit.Search;
using NineStore.BL.BaseBL;
using NineStore.BL.ProductBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.DL.OrderDL;
using NineStore.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.OrderBL
{
    
    public class OrderBL : BaseBL<Order>, IOrderBL
    {
        #region Field

        private IOrderDL _orderDL;

        #endregion
        #region Constructor
        public OrderBL(IOrderDL orderDL) : base(orderDL)
        {
            _orderDL = orderDL;
        }

        public List<OrderDetailService> GetAllOrderDetailService()
        {
            List<OrderDetailService> results = _orderDL.GetAllOrderDetailService();
            return results;
        }

        public List<OrderDetailService> GetOrderDetailServices(Guid? orderId)
        {
            List<OrderDetailService> results = _orderDL.GetOrderDetailService(orderId);
            return results;
        }

        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyWord)
        {

            dynamic dataRecord = _orderDL.GetRecordByFilterAndPaging(pageSize, pageNumber, keyWord);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<OrderDetailService>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(Math.Ceiling(totalPage)),
                TotalRecord = dataRecord[1],
                Data = dataRecord[0],
            };
        }

        public dynamic GetRecordByFilterAndUserId(int pageSize, int pageNumber, string? keyWord, Guid? userId)
        {

            dynamic dataRecord = _orderDL.GetRecordByFilterAndUserId(pageSize, pageNumber, keyWord,userId);
            double totalPage = Convert.ToDouble(dataRecord[1]) / pageSize;

            return new PagingResult<OrderDetailService>
            {
                CurrentPage = pageNumber,
                CurrentPageRecords = pageSize,
                TotalPage = Convert.ToInt32(Math.Ceiling(totalPage)),
                TotalRecord = dataRecord[1],
                Data = dataRecord[0],
            };
        }
        #endregion

    }
}
