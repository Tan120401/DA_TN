using Dapper;
using MailKit.Search;
using MySqlConnector;
using NineStore.Common.Constants;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.OrderDL
{
    public class OrderDL : BaseDL<Order>, IOrderDL
    {
        public List<OrderDetailService> GetAllOrderDetailService()
        {
            string storedProcedureName = "Proc_Order_GetAllService";

            List<OrderDetailService> listRecords;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                listRecords = result.Read<OrderDetailService>().ToList();
            }
            // Xử lý kết quả trả về
            // Thành công
            return listRecords;
        }

        public List<OrderDetailService> GetOrderDetailService(Guid? orderId)
        {
            string storedProcedureName = "Proc_Order_GetService";

            var parameters = new DynamicParameters();
            parameters.Add("p_OrderId", orderId);
            List<OrderDetailService> listRecords;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                listRecords = result.Read<OrderDetailService>().ToList();
            }
            // Xử lý kết quả trả về
            // Thành công
            return listRecords;
        }

        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword)
        {
            string storedProcedureName = String.Format(ProcedureName.Filter, typeof(OrderDetailService).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_KeyWord", keyword);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PageNumber", pageNumber);


            dynamic records;
            int totalRecord;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                totalRecord = result.Read<int>().Single();
                records = result.Read<OrderDetailService>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }


        dynamic IOrderDL.GetRecordByFilterAndUserId(int pageSize, int pageNumber, string? keyword,Guid? userId)
        {
            string storedProcedureName = String.Format(ProcedureName.Filter, typeof(Order).Name);

            var parameters = new DynamicParameters();
            parameters.Add("p_KeyWord", keyword);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_UserId", userId);


            dynamic records;
            int totalRecord;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                totalRecord = result.Read<int>().Single();
                records = result.Read<OrderDetailService>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }
    }
}
