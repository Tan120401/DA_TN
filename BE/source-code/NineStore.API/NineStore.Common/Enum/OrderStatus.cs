using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Enum
{
    public enum OrderStatus
    {
        /// <summary>
        /// Chờ xác nhận
        /// </summary>
        pedding = 0,
        /// <summary>
        /// Đã xác nhận
        /// </summary>
        approved = 1,
        /// <summary>
        /// Từ chối
        /// </summary>
        reject = 2,
    }
}
