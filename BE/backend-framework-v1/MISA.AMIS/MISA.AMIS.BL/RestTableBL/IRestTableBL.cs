using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.RestTableBL
{
    
    public interface IRestTableBL
    {
        #region Method

        /// <summary>
        /// Hàm lấy ra danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        List<RestTable> GetAllRestTable();

        /// <summary>
        /// Hàm xóa danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        int DeleteRestTable();

        /// <summary>
        /// Hàm chèn thông tin cột 
        /// </summary>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        int InsertMupltyRestTable(List<RestTable> restTables); 

        #endregion
    }


}
