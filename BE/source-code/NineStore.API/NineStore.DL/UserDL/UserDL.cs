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
    public class UserDL : BaseDL<UserRequest>, IUserDL
    {
        public int CheckUserName(string userName, Guid? userId)
        {
            string storedProcedureName = "Proc_Check_UserName";

            var parameters = new DynamicParameters();
            parameters.Add("p_UserName", userName);
            parameters.Add("p_UserId", userId);

            int result;

            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var mult = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                result = mult.Read<int>().Single();

            }
            return result;

        }


        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="email">bản ghi muốn sửa</param>
        /// <returns>
        /// 1: Nếu update thành công
        /// 0: Nếu update thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        public int ForgotPassword(string userName,string newPassword)
        {
            // procedurename
            string storedProcedureName = "Proc_User_ForgotPassword";

            var parameters = new DynamicParameters();
            parameters.Add("p_UserName", userName);
            parameters.Add("p_NewPassword", newPassword);
            int numberOfRowsAffect;

            // Khởi tạo kết nối DB
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Gọi proc
                numberOfRowsAffect = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffect;
            }
        }

        

        public List<UserRequest> LoginResult(UserRequest request)
        {
            // Chuẩn bị tên stored
            string storedProcedureName = ProcedureName.Login;

            // Chuẩn bị tham số đầu vào cho stored

            var parameters = new DynamicParameters();
            parameters.Add("p_username", request.UserName);
            parameters.Add("p_password", request.PassWord);

            // Khởi tạo kết nối đến DB

            dynamic result;

            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                // Gọi vào DB
                var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                result = multy.Read<UserRequest>().ToList();
            }
            return result;
        }
    }
}
