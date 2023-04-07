using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities.DTO
{
    public class ExportResult
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string? EmployeeFilter { get; set; }

        /// <summary>
        /// Số bản ghi trên một trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int PageNumber { get; set; }
    }
}
