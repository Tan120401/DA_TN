using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.RestDetailDL;
using MISA.AMIS.DL.RestDetailDL;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.RestDetailBL
{
    public class RestDetailBL : BaseBL<RestDetail>, IRestDetailBL
    {

        #region Field

        private IRestDetailDL _restDetailDL;

        #endregion

        #region Constructor

        public RestDetailBL(IRestDetailDL restDetailDL) : base(restDetailDL)
        {

            _restDetailDL = restDetailDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API lấy danh đơn lọc theo trang
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm</param>
        /// <param name="statusId">Từ khóa tìm kiếm theo trạng thái</param>
        /// <param name="departmentId">Từ khóa tìm kiếm theo phòng ban</param>
        /// <param name="filterParams">Đối tượng lọc nâng cao</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <returns>Danh sách đơn</returns
        /// Created by: NVTan (14/03/2023)
        public PagingResult GetRestDetailsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] string? statusId,
            [FromQuery] string? departmentId,
            List<FilterParams>? filterParams,
            [FromQuery] int pageSize = 50,
            [FromQuery] int pageNumber = 1
            )

        {
            var result = new PagingResult();
            string sql="";
            string queryString = "";
            foreach (var item in filterParams)
            {
                if (item.DataType == "date")
                {
                    if(item.Operator != null)
                    {
                        sql = String.Format("CAST(DATE_FORMAT({0}, '%Y/%m/%d') AS char) {1} '{2}' AND ", item.Field, item.Operator, item.Value);
                    }
                }
                else if (item.DataType == "number")
                {
                    if (item.Operator != null)
                    {
                        sql = String.Format("{0} {1} '{2}' AND ", item.Field, item.Operator, item.Value);
                    }
                }
                else
                {
                    if (item.Operator == "%")
                    {
                        sql = String.Format("{0} LIKE '%{1}%' AND ", item.Field, item.Value);
                    }
                    else if (item.Operator == "!%")
                    {
                        sql = String.Format("{0} NOT LIKE '%{1}%' AND ", item.Field, item.Value);
                    }
                    else
                    {
                        sql = String.Format("{0} {1} '{2}' AND ", item.Field, item.Operator, item.Value);
                    }
                }
                queryString += sql;
            }

            result = _restDetailDL.GetRestDetailsByFilter(keyword, statusId, departmentId, queryString, pageSize, pageNumber);

            if (result.TotalRecord % pageSize == 0)
            {
                result.TotalPage = result.TotalRecord / pageSize;
            }
            else
            {
                result.TotalPage = (result.TotalRecord / pageSize) + 1;
            }
            return result;
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần xóa </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        public int DeleteRestDetailMultiple(List<Guid> restDetailIds)
        {
            var result = _restDetailDL.DeleteRestDetailMultiple(restDetailIds);
            return result;
        }

        /// <summary>
        /// Hàm cập nhật trạng thái nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần cập nhật trạng thái </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        public int UpdateRestDetailMultiple(int statusId, List<Guid> restDetailIds)
        {
            var result = _restDetailDL.UpdateRestDetailMultiple(statusId, restDetailIds);
            return result;
        }

        /// <summary>
        /// Hàm xuất khẩu dữ liệu
        /// </summary>
        /// <param name="RestDetailIds">danh sách id đơn cần xuất khẩu</param>
        /// <returns>
        /// Nếu có id thì xuất khẩu theo danh sách id
        /// Không có id thì xuất khẩu tất cả
        /// </returns>
        /// Created By: NVTAN (09/03/2023)
        public dynamic ExportToExcel(List<Guid>? RestDetailIds)
        {
            var listRestDetails = _restDetailDL.ExportToExcel(RestDetailIds);

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Đơn đăng ký làm thêm");
                workSheet.Cells.LoadFromCollection(listRestDetails, false);
                FomatStyleForExcel(listRestDetails, workSheet);
                package.Save();
            }

            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Hàm định dạng style của file excel
        /// </summary>
        /// <param name="listRestDetails">danh sách bản ghi</param>
        /// <param name="workSheet">sheet trong excel</param>
        /// Created By NVTAN (09/03/2023)
        private static void FomatStyleForExcel(List<RestDetail> listRestDetails, ExcelWorksheet workSheet)
        {

            workSheet.Cells["A1:S1"].Merge = true;
            workSheet.Cells[1, 1].Value = "";
            workSheet.Cells["A2:S2"].Merge = true;
            workSheet.Cells["A2:S2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[2, 1].Value = "ĐƠN XIN NGHỈ";
            workSheet.Cells[2, 1].Style.Font.Bold = true;
            workSheet.Cells["A2"].Style.Font.Size = 16;
            workSheet.Cells["A2"].Style.Font.Name = "Arial";
            workSheet.Cells["A3:S3"].Value = "";

            int number = listRestDetails.Count + 5;
            workSheet.Cells[$"A4:S{number}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A4:S{number}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A4:S{number}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[$"A4:S{number}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Thiết lập header

            workSheet.Cells["A4:A5"].Merge = true;
            workSheet.Cells[4, 1].Value = "STT";
            workSheet.Cells["B4:Q4"].Merge = true;
            workSheet.Cells[4, 2].Value = "Thông tin chung";
            workSheet.Cells["A4:P4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["R4:S4"].Merge = true;
            workSheet.Cells[4, 18].Value = "Danh sách nhân viên xin nghỉ";
            workSheet.Cells["R4:S4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells["A4:A5"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells[5, 2].Value = "Mã nhân viên";
            workSheet.Cells[5, 3].Value = "Người nộp đơn";
            workSheet.Cells[5, 4].Value = "Vị trí công việc";
            workSheet.Cells[5, 5].Value = "Đơn vị công tác";
            workSheet.Cells[5, 6].Value = "Từ ngày";
            workSheet.Cells[5, 7].Value = "Đến ngày";
            workSheet.Cells[5, 8].Value = "Số ngày nghỉ";
            workSheet.Cells[5, 9].Value = "Số giờ nghỉ";
            workSheet.Cells[5, 10].Value = "Loại nghỉ";
            workSheet.Cells[5, 11].Value = "Tỷ lệ hưởng lương";
            workSheet.Cells[5, 12].Value = "Lý do nghỉ";
            workSheet.Cells[5, 13].Value = "Người duyệt";
            workSheet.Cells[5, 14].Value = "Người thay thế";
            workSheet.Cells[5, 15].Value = "Người liên quan";
            workSheet.Cells[5, 16].Value = "Ghi chú";
            workSheet.Cells[5, 17].Value = "Trạng thái";
            workSheet.Cells[5, 18].Value = "Mã nhân viên xin nghỉ";
            workSheet.Cells[5, 19].Value = "Nhân viên xin nghỉ";

            using (var range = workSheet.Cells["A4:S4"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
            }
            using (var range = workSheet.Cells["A5:S5"])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                range.Style.Font.Bold = true;
            }

            for (int i = 0; i < listRestDetails.Count; i++)
            {
                workSheet.Column(i + 1).AutoFit();
                var item = listRestDetails[i];
                workSheet.Cells[i + 6, 1].Value = i + 1;
                workSheet.Cells[i + 6, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 6, 2].Value = item.EmployeeCode;
                workSheet.Cells[i + 6, 3].Value = item.FullName;
                workSheet.Cells[i + 6, 4].Value = item.PositionName;
                workSheet.Cells[i + 6, 5].Value = item.DepartmentName;
                workSheet.Cells[i + 6, 6].Value = item.DateFrom;
                workSheet.Cells[i + 6, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 6, 7].Value = item.DateTo;
                workSheet.Cells[i + 6, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells[i + 6, 8].Value = item.NumRest;
                workSheet.Cells[i + 6, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[i + 6, 9].Value = item.HourRest;
                workSheet.Cells[i + 6, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[i + 6, 10].Value = item.TypeRestName;
                workSheet.Cells[i + 6, 11].Value = item.SalaryPercent;
                workSheet.Cells[i + 6, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells[i + 6, 12].Value = item.ReasonRest;
                workSheet.Cells[i + 6, 13].Value = item.ApplyPersonName;
                workSheet.Cells[i + 6, 14].Value = item.SwapPersonName;
                workSheet.Cells[i + 6, 15].Value = item.RelatePersonName;
                workSheet.Cells[i + 6, 16].Value = item.NoteRest;
                workSheet.Cells[i + 6, 17].Value = item.StatusName;
                workSheet.Cells[i + 6, 18].Value = "";
                workSheet.Cells[i + 6, 19].Value = "";
                workSheet.Cells["T:X"].Clear();
            }
            workSheet.Column(1).Width = 5;
            workSheet.Column(2).Width = 15;
            workSheet.Column(6).Width = 22;
            workSheet.Column(7).Width = 22;
            workSheet.Column(8).Width = 22;
            workSheet.Column(9).Width = 22;
            workSheet.Column(10).Width = 22;
            workSheet.Column(16).Width = 15;
            workSheet.Column(18).Width = 20;

            workSheet.Cells[6, 6, listRestDetails.Count + 6, 6].Style.Numberformat.Format = "DD/MM/YYYY hh:mm:ss";
            workSheet.Cells[6, 7, listRestDetails.Count + 6, 7].Style.Numberformat.Format = "DD/MM/YYYY hh:mm:ss";
        } 

        #endregion
    }
}
