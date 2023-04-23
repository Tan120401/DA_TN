using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.UserBL
{
    public interface IUserBL: IBaseBL<UserRequest>
    {
        List<UserRequest> LoginResult(UserRequest request);
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="email">bản ghi muốn sửa</param>
        /// <returns>
        /// 1: Nếu sửa thành công
        /// 0: Nếu sửa thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        ServiceResult ForgotPassword(string userName, string newPassword);
    }
}
