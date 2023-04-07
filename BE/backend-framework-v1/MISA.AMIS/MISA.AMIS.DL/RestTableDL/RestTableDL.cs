using Dapper;
using MISA.AMIS.Common.Constrants;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.RestTableDL
{
    public class RestTableDL : IRestTableDL
    {
        #region Method

        /// <summary>
        /// Hàm lấy ra danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        public List<RestTable> GetAllRestTable()
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(RestTable).Name, "All");

            // Khởi tạo kết nối tới Database
            List<RestTable> listRestTable;
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                listRestTable = result.Read<RestTable>().ToList();
            }
            // Xử lý kết quả trả về
            // Thành công
            return listRestTable;
        }

        /// <summary>
        /// Hàm xóa danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        public int DeleteRestTable()
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.Delete, typeof(RestTable).Name);

            // Khởi tạo kết nối tới Database
            int numberAffectedRows;
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                numberAffectedRows = mySqlConnection.Execute(storedProcedureName, commandType: CommandType.StoredProcedure);
            }
            // Xử lý kết quả trả về
            // Thành công
            return numberAffectedRows;
        }

        /// <summary>
        /// Hàm chèn thông tin cột 
        /// </summary>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        public int InsertMupltyRestTable(List<RestTable> restTables)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Insert, typeof(RestTable).Name);
            //Chuẩn bị tham số đầu vào cho store
            var parameters = new DynamicParameters();
            var properties = typeof(RestTable).GetProperties();
            var numberOfAffectedRows = 0;
            foreach (var restTable in restTables)
            {
                foreach (var prop in properties)
                {
                    parameters.Add($"p_{prop.Name}", prop.GetValue(restTable));
                }
                using (var mysqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
                {
                    //Gọi vào DB
                    numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            return numberOfAffectedRows;
        } 

        #endregion
    }
}
