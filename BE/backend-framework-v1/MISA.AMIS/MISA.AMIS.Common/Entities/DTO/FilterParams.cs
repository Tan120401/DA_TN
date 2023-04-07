using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities.DTO
{
    public class FilterParams
    {
        /// <summary>
        /// Trường dữ liệu cần lọc
        /// </summary>
        public string? Field { get; set; }

        /// <summary>
        /// Toán tử lọc
        /// </summary>
        public string? Operator { get; set; }

        /// <summary>
        /// Giá trị cần lọc
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Kiểu dữ liệu trường cần lọc
        /// </summary>
        public string? DataType { get; set; }
    }
}
