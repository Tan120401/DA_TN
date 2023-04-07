﻿using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.RestDetailDL
{
    public interface IRestDetailDL:IBaseDL<RestDetail>
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
        PagingResult GetRestDetailsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] string? statusId,
            [FromQuery] string? departmentId,
             string? filterParams,
            [FromQuery] int pageSize = 50,
            [FromQuery] int pageNumber = 1);

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần xóa </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        public int DeleteRestDetailMultiple(List<Guid> restDetailIds);

        /// <summary>
        /// Hàm sửa nhiều bản ghi
        /// </summary>
        /// <param name="restDetailIds">danh sách Id bản ghi cần sửa </param>
        /// <returns></returns>
        /// Author : NVTAN (08/02/2023)
        public int UpdateRestDetailMultiple(int statusId, List<Guid> restDetailIds);

        /// <summary>
        /// Hàm xuất khẩu dữ liệu
        /// </summary>
        /// <param name="RestDetailIds">danh sách id đơn cần xuất khẩu</param>
        /// <returns>
        /// Nếu có id thì xuất khẩu theo danh sách id
        /// Không có id thì xuất khẩu tất cả
        /// </returns>
        /// Created By: NVTAN (09/03/2023)
        List<RestDetail> ExportToExcel(List<Guid>? RestDetailIds); 

        #endregion
    }
}
