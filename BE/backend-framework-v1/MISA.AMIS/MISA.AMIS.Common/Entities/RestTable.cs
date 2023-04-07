using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class RestTable
    {
        /// <summary>
        /// Trường dữ liệu
        /// </summary>
        public string? Field { get; set; }
        
        /// <summary>
        /// Tên cột
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// Độ rộng cột
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Thuộc tính ghim
        /// </summary>
        public bool? Fixed { get; set; }

        /// <summary>
        /// Tên template custom
        /// </summary>
        public string? Template { get; set; }

        /// <summary>
        /// Căn chỉnh cột
        /// </summary>
        public string? Alignment { get; set; }

        /// <summary>
        /// Định dạng dữ liệu cột
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Kiểu dữ liệu cột
        /// </summary>
        public string? DataType { get; set; }

        /// <summary>
        /// Thuộc tính xác nhận hiện cột hay không
        /// </summary>
        public bool? IsCheck { get; set; }

        /// <summary>
        /// Thứ tự cột
        /// </summary>
        public int? TabOrder { get; set; }
    }
}
