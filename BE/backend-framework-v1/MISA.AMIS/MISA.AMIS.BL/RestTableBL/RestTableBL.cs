using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.RestTableDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.RestTableBL
{
    
    public class RestTableBL : IRestTableBL
    {
        #region Field

        private IRestTableDL _restTableDL;

        #endregion

        #region Constructor

        public RestTableBL(IRestTableDL restTableDl)
        {
            _restTableDL =  restTableDl;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy ra danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        public List<RestTable> GetAllRestTable()
        {
            List<RestTable> result;
            result = _restTableDL.GetAllRestTable();
            return result;
        }

        /// <summary>
        /// Hàm xóa danh sách cột cho bảng đơn
        /// </summary>
        /// <returns></returns>
        public int DeleteRestTable()
        {
            int numAffectedRows = _restTableDL.DeleteRestTable();
            return numAffectedRows;
        }

        /// <summary>
        /// Hàm chèn thông tin cột 
        /// </summary>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        public int InsertMupltyRestTable(List<RestTable> restTables)
        {
            int numAffectedRows = _restTableDL.InsertMupltyRestTable(restTables);
            return numAffectedRows;
        } 

        #endregion
    }
}
