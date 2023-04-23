using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
using NineStore.Common.Entities.DTO;
using NineStore.Common.Enum;
using NineStore.Common.Resource;
using NineStore.DL.UserDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.UserBL
{
    public class UserBL : BaseBL<UserRequest>, IUserBL
    {
        #region Field

        private IUserDL _userDL;

        #endregion

        #region Constructor

        public UserBL(IUserDL userDL) : base(userDL)
        {
            _userDL = userDL;
        }

        public ServiceResult ForgotPassword(string userName, string newPassword)
        {
            int result = _userDL.CheckUserName(userName, Guid.Empty);
            int numberAffectedRow = _userDL.ForgotPassword(userName, newPassword);
            if (result < 1)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.NotExistUserName,
                    Data = Resource.NotExistUserName
                };
            }
            else if(numberAffectedRow > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = ErrorCode.NoData,
                    Data = "Lỗi khi gọi vào DL",
                };
            }
            
        }

        #endregion

        public List<UserRequest> LoginResult(UserRequest user)
        {
            List<UserRequest> result = _userDL.LoginResult(user);
            return result;
        }


        /// <summary>
        /// Hàm valdate ghi đè ở baseBL
        /// </summary>
        /// <param name="record">Nhân viên muốn kiểm tra validate</param>
        /// <returns>ServiceResult</returns>
        protected override ServiceResult ValidateCustom(UserRequest? record)
        {
            ServiceResult lstValiDateCustom = new ServiceResult();
            int result = _userDL.CheckUserName(record.UserName, record.UserId);
            if (result > 0)
            {
                lstValiDateCustom.IsSuccess = false;
                lstValiDateCustom.ErrorCode = ErrorCode.DuplicateCode;
                lstValiDateCustom.Data = Resource.ErrorDuplicate;
            }
            else
            {
                lstValiDateCustom = null;
            }
            return lstValiDateCustom;
        }
    }
}
