using Dapper;
using MySqlConnector;
using NineStore.Common.Constants;
using NineStore.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Method

        /// <summary>
        /// Lấy danh sách record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: NVTan (09/02/2023)
        public virtual List<T> GetAllRecord()
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = String.Format(ProcedureName.Get, typeof(T).Name, "All");

            // Khởi tạo kết nối tới Database
            List<T> listRecords;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
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
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
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
        public int InsertRecord(T record, string? imgName)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Insert, typeof(T).Name);
            //Chuẩn bị tham số đầu vào cho store
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            Guid keyValue = Guid.Empty;
            foreach (var prop in properties)
            {
                if (prop.Name == "ImgName")
                {
                    parameters.Add($"p_{prop.Name}", imgName);
                }
                else
                {
                    parameters.Add($"p_{prop.Name}", prop.GetValue(record));
                }
                var keyAttribute = (KeyAttribute?)prop.GetCustomAttributes(typeof(KeyAttribute), false).FirstOrDefault();
                if(keyAttribute != null)
                {
                    if (prop.GetValue(record) != null)
                    {
                        keyValue = (Guid)prop.GetValue(record);
                    }
                }
            }
            if(keyValue != Guid.Empty)
            {
                GeneratePrimaryKey(parameters, properties, keyValue); 
            }
            else
            {
                GeneratePrimaryKey(parameters, properties, null);
            }
            //Khởi tạo kết nối tới DB
            int numberOfAffectedRows;
            using (var mysqlConnection = new MySqlConnection(DataContext.ConnectionString))
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
        public int UpdateRecord(Guid recordId, T record, string? imgName)
        {
            // procedurename
            string storedProcedureName = string.Format(ProcedureName.Update, typeof(T).Name);

            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();

            //Chuẩn bị tham số đầu vào
            foreach (var prop in properties)
            {
                if(prop.Name == "ImgName")
                {
                    parameters.Add($"p_{prop.Name}", imgName);
                }
                else
                {
                    parameters.Add($"p_{prop.Name}", prop.GetValue(record));
                }
            }

            GeneratePrimaryKey(parameters, properties, recordId);
            // Khởi tạo kết nối DB
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
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
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Thực hiện gọi vào Database để chạy stored procedure
                numberOfAffectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            }
            return numberOfAffectedRows;
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần xóa </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        public int DeleteRecordMulpty(List<Guid> recordIds)
        {
            // khởi tạo câu lệnh sql
            var sql = "DELETE FROM {0} WHERE {1}Id IN('{2}')";
            var result = 0;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                mySqlConnection.Open();

                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        result = mySqlConnection.Execute(string.Format(sql,typeof(T).Name, typeof(T).Name, string.Join("','", recordIds)), transaction: transaction);

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
                mySqlConnection.Close();
            }
            return result;
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

        public dynamic GetRecordByFilterAndPaging(int pageSize, int pageNumber, string? keyword)
        {
            string storedProcedureName = String.Format(ProcedureName.Filter, typeof(T).Name);

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
                records = result.Read<T>().ToList();
            }

            List<dynamic> dataRecords = new List<dynamic>() { records, totalRecord };

            return dataRecords;
        }

        #endregion

    }
}
