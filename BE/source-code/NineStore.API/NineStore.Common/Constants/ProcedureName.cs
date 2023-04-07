using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Constants
{
    public static class ProcedureName
    {
        public static string Login = "Proc_Login";
        /// <summary>
        /// ProcedureName getbyid
        /// </summary>
        public static string Get = "Proc_{0}_Get{1}";

        /// <summary>
        /// ProcedureName insert
        /// </summary>
        public static string Insert = "Proc_{0}_Insert";

        /// <summary>
        /// ProcedureName update
        /// </summary>
        public static string Update = "Proc_{0}_Update";

        /// <summary>
        /// ProcedureName delete
        /// </summary>
        public static string Delete = "Proc_{0}_Delete";

        /// <summary>
        /// ProcedureName phân trang và tìm kiếm
        /// </summary>
        public static string Filter = "Proc_{0}_Search_Paging";

        /// <summary>
        /// ProcedureName sửa nhiều
        /// </summary>
        public static string UpdateMulpty = "UPDATE restdetail r SET r.StatusId = ('{0}')  WHERE RestDetailId IN ('{1}')";

        /// <summary>
        /// ProcedureName xuất khẩu dữ liệu
        /// </summary>
        public static string ExportToExcel = "SELECT r.RestdetailId, e.FullName,e.EmployeeCode, e.PositionName, r.SubmitDate, d.DepartmentName, r.DateFrom, r.DateTo,\r\n  r.NumRest, r.HourRest, r.TypeRest,\r\n  CASE \r\n  WHEN r.TypeRest = 0 THEN \"Nghỉ phép\" \r\n  WHEN r.TypeRest = 1 THEN \"Nghỉ bù\"\r\n  ELSE \"Nghỉ kết hôn\"\r\n  END AS TypeRestName\r\n  , r.SalaryPercent, r.ReasonRest, r.ApplyPersonName, r.SwapPersonName, r.RelatePersonName, r.NoteRest,r.StatusId,\r\n  CASE \r\n  WHEN r.StatusId = 0 THEN \"Đã duyệt\" \r\n  WHEN r.StatusId = 1 THEN \"Chờ duyệt\"\r\n  ELSE \"Từ chối\"\r\n  END AS StatusName\r\n  FROM\r\n  restdetail r LEFT JOIN employee e ON r.EmployeeId = e.EmployeeId LEFT JOIN department d ON e.DepartmentId = d.DepartmentId WHERE RestDetailId IN ('{0}')";


        /// <summary>
        /// ProcedureName xóa nhiều
        /// </summary>
        public static string DeleteMulpty = "DELETE FROM restdetail WHERE RestDetailId IN('{0}')";
    }
}
