using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class RestDetail
    {
        /// <summary>
        /// Id đơn nghỉ
        /// </summary>
        [Key]
        public Guid? RestDetailId { get; set; }

        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Required(ErrorMessage = "Người nộp đơn không được bỏ trống")]
        public Guid? EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        public string? PositionName { get; set; }


        /// <summary>
        /// Ngày nộp đơn
        /// </summary>
        [Required(ErrorMessage = "Ngày nộp đơn không được bỏ trống")]
        public DateTime? SubmitDate { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        [Required(ErrorMessage = "Từ ngày không được bỏ trống")]
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        [Required(ErrorMessage = "Đến ngày không được bỏ trống")]
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Số ngày nghỉ
        /// </summary>
        public int? NumRest { get; set; }

        /// <summary>
        /// Số giờ nghỉ
        /// </summary>
        public int? HourRest { get; set; }
        /// <summary>
        /// Loại nghỉ
        /// </summary>
        [Required(ErrorMessage = "Loại nghỉ không được bỏ trống")]
        public int? TypeRest { get; set; }

        /// <summary>
        /// Tên loại nghỉ
        /// </summary>
        public string? TypeRestName { get; set; }


        /// <summary>
        /// Tỷ lệ hưởng lươngs
        /// </summary>
        public int? SalaryPercent { get; set; }

        /// <summary>
        /// Lý do nghỉ
        /// </summary>
        [Required(ErrorMessage = "Lý do nghỉ không được bỏ trống")]
        public string? ReasonRest { get; set; }

        /// <summary>
        /// Id Người duyệt
        /// </summary>
        [Required(ErrorMessage = "Mã người duyệt không được bỏ trống")]
        public Guid? ApplyPersonId { get; set; }

        /// <summary>
        /// Người duyệt
        /// </summary>
        public string ApplyPersonName { get; set; }

        /// <summary>
        /// Id Người thay thế
        /// </summary>
        [Required(ErrorMessage = "Mã người thay thế không được bỏ trống")]
        public Guid? SwapPersonId { get; set; }

        /// <summary>
        /// Người thay thế
        /// </summary>
        public string SwapPersonName { get; set; }

        /// <summary>
        /// Id người liên quan
        /// </summary>
        public Guid? RelatePersonId { get; set; }

        /// <summary>
        /// Người liên quan
        /// </summary>
        public string RelatePersonName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? NoteRest { get; set; }

        /// <summary>
        /// Kiểu trạng thái
        /// </summary>
        [Required(ErrorMessage = "Trạng thái không được bỏ trống")]
        public int? StatusId { get; set; }

        /// <summary>
        /// Tên trạng thái
        /// </summary>
        public string? StatusName { get; set; }
    }
}
