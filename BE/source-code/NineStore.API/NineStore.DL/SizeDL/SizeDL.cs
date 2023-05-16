using Dapper;
using MySqlConnector;
using NineStore.Common.Constants;
using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.SizeDL
{
    public class SizeDL : BaseDL<Size>, ISizeDL
    {
        public List<Size> GetSizeByProductId(Guid recordId)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Get, typeof(Size).Name, "ByProductId");

            // Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            parameters.Add("p_ProductId", recordId);

            // Khởi tạo kết nối tới Database
            dynamic record;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                record = result.Read<Size>().ToList();
            }
            return record;
        }

        public int InsertMupltySize(List<Size> Sizes)
        {
            //Chuẩn bị tên stored procedure
            string storedProcedureName = string.Format(ProcedureName.Insert, typeof(Size).Name);
            //Chuẩn bị tham số đầu vào cho store
            var parameters = new DynamicParameters();
            var properties = typeof(Size).GetProperties();
            var numberOfAffectedRows = 0;
            foreach (var Size in Sizes)
            {
                foreach (var prop in properties)
                {
                    parameters.Add($"p_{prop.Name}", prop.GetValue(Size));
                }
                GeneratePrimaryKey(parameters, properties, null);
                using (var mysqlConnection = new MySqlConnection(DataContext.ConnectionString))
                {
                    //Gọi vào DB
                    numberOfAffectedRows = mysqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            return numberOfAffectedRows;
        }


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
    }
}
