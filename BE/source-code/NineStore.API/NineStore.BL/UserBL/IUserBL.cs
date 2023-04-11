﻿using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
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
    }
}
