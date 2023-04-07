using Dapper;
using MySqlConnector;
using NineStore.Common.Constants;
using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.UserDL
{
    public class UserDL : BaseDL<User>, IUserDL
    {
        public int CheckUserName(string userName)
        {
            string storedProcedureName = "Proc_Check_UserName";

            var parameters = new DynamicParameters();
            parameters.Add("p_UserName", userName);

            int result;
            
            using(var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var mult = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                result = mult.Read<int>().Single();

            }
            return result;

        }

        public int LoginResult(User user)
        {
            // Chuẩn bị tên stored
            string storedProcedureName = ProcedureName.Login;

            // Chuẩn bị tham số đầu vào cho stored

            var parameters = new DynamicParameters();
            parameters.Add("p_username", user.UserName);
            parameters.Add("p_password", user.PassWord);

            // Khởi tạo kết nối đến DB

            int numberOfAffectedRows;

            using(var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Gọi vào DB
                var result = mySqlConnection.QueryMultiple(storedProcedureName, parameters,commandType: System.Data.CommandType.StoredProcedure);

                numberOfAffectedRows = result.Read<int>().Single();
            }

            return numberOfAffectedRows;
        }
    }
}
