using MISA.AMIS.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MISA.AMIS.Common.Constrants;
namespace MISA.AMIS.Common.Entities
{
    /*
     thông tin nhân viên
     */
    public class Employee
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string FullName { get; set; }


        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }


        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gân nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gân nhất
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
