﻿using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities.DTO
{
    public class ServiceResult
    {
        /// <summary>
        /// Kết quả trả về thành không hay không
        /// </summary>
        public bool IsSuccess { get; set; }

        public ErrorCode? ErrorCode { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}
