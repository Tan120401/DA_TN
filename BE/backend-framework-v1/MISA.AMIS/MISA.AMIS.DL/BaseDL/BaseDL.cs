using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Constrants;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Method

        /// <summary>
        /// Lấy danh sách record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: NVTan (09/02/2023)
        public List<T> GetAllRecord()
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(T).Name, "All");

            // Khởi tạo kết nối tới Database
            List<T> listRecords;
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                listRecords = result.Read<T>().ToList();
            }
            // Xử lý kết quả trả về
            // Thành công
            return listRecords;
        }

        /// <summary>
        /// Tìm bản ghi theo ID
        /// </summary>
        /// <param name="recordId">ID bản ghi cần tìm kiếm</param>
        /// <returns>Bản ghi cần tìm kiếm</returns>
        /// Created by: NVTan (16/01/2023)
        public List<T> GetRecordById(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Get, typeof(T).Name, "ById");

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("p_" + typeof(T).Name + "Id", recordId);

            // Khởi tạo kết nối tới Database
            dynamic record;
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                record = result.Read<T>().ToList();
            }
            return record;
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created By: NVTAN (09/02/2023)
        public int InsertRecord(T record)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Insert, typeof(T).Name);
            //Chuẩn bị tham số đầu vào cho store
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(record));
            }
            GeneratePrimaryKey(parameters, properties, null);
            //Khởi tạo kết nối tới DB
            int numberOfAffectedRows;
            using (var mysqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                //Gọi vào DB
                numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn sửa</param>
        /// <param name="record">bản ghi muốn sửa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        public int UpdateRecord(Guid recordId, T record)
        {
            // procedurename
            string storedProcedureName = string.Format(ProcedureName.Update, typeof(T).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            //Chuẩn bị tham số đầu vào
            foreach (var prop in properties)
            {
                parameters.Add($"p_{prop.Name}", prop.GetValue(record));

            }
            GeneratePrimaryKey(parameters, properties, recordId);
            // Khởi tạo kết nối DB
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {

                // Gọi proc
                var numberOfRowsAffect = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffect;

            }
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn xóa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        public int DeleteRecord(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Delete, typeof(T).Name);

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("p_" + typeof(T).Name + "Id", recordId);
            // Khởi tạo kết nối tới Database
            int numberOfAffectedRows;
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                // Thực hiện gọi vào Database để chạy stored procedure
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Sinh dữ liệu cho khóa chính
        /// </summary>
        /// <param name="parameters">Các tham số đầu vào</param>
        /// <param name="properties">Các thuộc tính của đối tượng</param>
        /// Created by: NVTAN (08/02/2023)
        protected virtual void GeneratePrimaryKey(DynamicParameters parameters, PropertyInfo[] properties, Guid? id)
        {
            foreach (var property in properties)
            {
                var keyAttribute = (KeyAttribute?)property.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if (keyAttribute != null)
                {
                    if (id != null)
                    {
                        parameters.Add($"p_{property.Name}", id);
                    }
                    else
                    {
                        parameters.Add($"p_{property.Name}", Guid.NewGuid());
                    }
                }
            }
        } 

        #endregion

    }
}
