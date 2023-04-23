using Dapper;
using MySqlConnector;
using NineStore.Common.Constants;
using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using NineStore.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.ProductDL
{
    public class ProductDL: BaseDL<Product>, IProductDL
    {
        /// <summary>
        /// Lấy danh sách record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: NVTan (09/02/2023)
        public  List<Product> GetAllProduct(Guid? categoryId, string? order)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(Product).Name, "All");

            var parameters = new DynamicParameters();
            parameters.Add("p_CategoryId", categoryId);
            parameters.Add("p_Order", order);
            // Khởi tạo kết nối tới Database
            List<Product> listRecords;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName,parameters, commandType: CommandType.StoredProcedure);
                listRecords = result.Read<Product>().ToList();
            }
            // Xử lý kết quả trả về
            // Thành công
            return listRecords;
        }
    }
}
