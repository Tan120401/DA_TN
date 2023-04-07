using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Constrants;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Resources;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.Datacontext;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.RestDetailDL
{
    public class RestDetaiDL : BaseDL<RestDetail>, IRestDetailDL
    {
        #region Method

        /// <summary>
        /// API lấy danh đơn lọc theo trang
        /// </summary>
        /// <param name="keyword">Từ khóa để tìm kiếm</param>
        /// <param name="statusId">Từ khóa tìm kiếm theo trạng thái</param>
        /// <param name="departmentName">Từ khóa tìm kiếm theo phòng ban</param>
        /// <param name="filterParams">Đối tượng lọc nâng cao</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <returns>Danh sách đơn</returns
        /// Created by: NVTan (14/03/2023)
        public PagingResult GetRestDetailsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] string? statusId,
            [FromQuery] string? departmentId,
             string? filterParams,
            [FromQuery] int pageSize = 50,
            [FromQuery] int pageNumber = 1)
        {

            // Khởi tạo kêt quả trả về
            var result = new PagingResult();

            //Chuẩn bị tên Stored Procedure
            string storedProcedureName = string.Format(ProcedureName.Filter, typeof(RestDetail).Name);

            // Chuẩn bị tham số đầu vào cho proc
            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageSize", pageSize);
            parameters.Add("p_Keyword", keyword);
            parameters.Add("p_StatusId", statusId);
            parameters.Add("p_departmentId", departmentId);
            parameters.Add("p_FilterParams", filterParams);

            // Khởi tạo kết nối db
            using (var mySqlConnection = new MySqlConnection(Datacontext.DataBaseContext.connectionString))
            {
                // Gọi proc
                var multy = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // lấy kết quả gán cho result
                result.TotalRecord = multy.Read<int>().Single();
                result.Data = multy.Read<RestDetail>().ToList();
                result.CurrentPageRecords = pageSize;
                result.CurrentPage = pageNumber;
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
            // khởi tạo câu lệnh sql
            var sql = ProcedureName.DeleteMulpty;
            var result = 0;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.connectionString))
            {
                mySqlConnection.Open();

                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        result = mySqlConnection.Execute(string.Format(sql, string.Join("','", restDetailIds)), transaction: transaction);

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
                mySqlConnection.Close();
            }
            return result;
        }

        /// <summary>
        /// Hàm sửa nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi sửa xóa </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        public int UpdateRestDetailMultiple(int statusId, List<Guid> restDetailIds)
        {
            // khởi tạo câu lệnh sql
            var sql = ProcedureName.UpdateMulpty;
            var result = 0;
            using (var mySqlConnection = new MySqlConnection(DataBaseContext.connectionString))
            {
                mySqlConnection.Open();

                using (var transaction = mySqlConnection.BeginTransaction())
                {
                    try
                    {
                        result = mySqlConnection.Execute(string.Format(sql, statusId, string.Join("','", restDetailIds)), transaction: transaction);
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                }
                mySqlConnection.Close();
            }
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
        public List<RestDetail> ExportToExcel(List<Guid>? RestDetailIds)
        {
            dynamic RestDetail;

            if (RestDetailIds.Count > 0)
            {
                string sql = ProcedureName.ExportToExcel;
                using (var mySqlConnection = new MySqlConnection(DataBaseContext.connectionString))
                {
                    var result = mySqlConnection.QueryMultiple(string.Format(sql, string.Join("','", RestDetailIds)));
                    RestDetail = result.Read<RestDetail>().ToList();
                }
            }
            else
            {
                string storedProcedureName = String.Format(ProcedureName.Get, typeof(RestDetail).Name, "All");
                using (var mySqlConnection = new MySqlConnection(DataBaseContext.connectionString))
                {
                    var result = mySqlConnection.QueryMultiple(storedProcedureName, commandType: CommandType.StoredProcedure);
                    RestDetail = result.Read<RestDetail>().ToList();
                }
            }
            return RestDetail;
        } 

        #endregion
    }
}
