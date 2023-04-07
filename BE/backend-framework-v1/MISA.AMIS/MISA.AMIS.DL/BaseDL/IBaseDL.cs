using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.BaseDL
{
    public interface IBaseDL<T>
    {
        #region Method

        /// <summary>
        /// Lấy danh sách record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: NVTan (09/02/2023)
        public List<T> GetAllRecord();

        /// <summary>
        /// Tìm bản ghi theo ID
        /// </summary>
        /// <param name="recordId">ID bản ghi cần tìm kiếm</param>
        /// <returns>Bản ghi cần tìm kiếm</returns>
        /// Created by: NVTan (16/01/2023)
        List<T> GetRecordById(Guid recordId);

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="record">Bản ghi muốn thêm</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created By: NVTAN (09/02/2023)
        int InsertRecord(T record);

        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn sửa</param>
        /// <param name="record">bản ghi muốn sửa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        int UpdateRecord(Guid recordId, T record);

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn xóa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        int DeleteRecord(Guid recordId); 

        #endregion
    }
}
