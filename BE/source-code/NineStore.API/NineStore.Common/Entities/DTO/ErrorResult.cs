﻿using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities.DTO
{
    public class ErrorResult
    {
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Message lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin về lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// TraceId lỗi
        /// </summary>
        public string TraceId { get; set; }
    }
}
