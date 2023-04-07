﻿using NineStore.BL.BaseBL;
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
    public class UserBL : BaseBL<User>, IUserBL
    {
        #region Field

        private IUserDL _userDL;

        #endregion

        #region Constructor

        public UserBL(IUserDL userDL):base(userDL)
        {
            _userDL = userDL;
        }

        #endregion

        public int LoginResult(User user)
        {
            int result = _userDL.LoginResult(user);
            return result;
        }

        /// <summary>
        /// Hàm valdate ghi đè ở baseBL
        /// </summary>
        /// <param name="record">Nhân viên muốn kiểm tra validate</param>
        /// <returns>ServiceResult</returns>
        protected override ServiceResult ValidateCustom(User? record)
        {
            ServiceResult lstValiDateCustom = new ServiceResult();
            int result = _userDL.CheckUserName(record.UserName);
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
