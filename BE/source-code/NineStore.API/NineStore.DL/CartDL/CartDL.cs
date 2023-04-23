using Dapper;
using MySqlConnector;
using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.CartDL
{
    public class CartDL : BaseDL<Cart>, ICartDL
    {
        public List<CartResponse> GetCartByUser(Guid? userId)
        {
            string storedProcedureName = "Proc_Cart_GetByUserId";
            var parameters = new DynamicParameters();
            parameters.Add("p_UserId", userId);
            List<CartResponse> results;
            using (var mySqlConnection = new MySqlConnection(DataContext.ConnectionString))
            {
                var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                results = multy.Read<CartResponse>().ToList();
            }
            return results;
        }
    }
}
