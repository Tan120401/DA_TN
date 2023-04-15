using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.UserDL
{
    public  interface IUserDL:IBaseDL<UserRequest>
    {
        /// <summary>
        /// Hàm login
        /// </summary>
        /// <param name="user"></param>
        /// <returns> 1: nếu đăng nhập thành công</returns>
        List<UserRequest> LoginResult(UserRequest request);

        /// <summary>
        /// Hàm check trùng tên đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>1: nếu đã trùng</returns>
        int CheckUserName(string userName, Guid? userId);

    }
}
