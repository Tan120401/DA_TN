using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.Common.Resources;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.EmployeeDL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.BL.BaseBL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Tìm bản ghi theo ID
        /// </summary>
        /// <param name="recordId">ID bản ghi cần tìm kiếm</param>
        /// <returns>Bản ghi cần tìm kiếm</returns>
        /// Created by: NVTan (16/01/2023)
        public List<T> GetRecordById(Guid recordId)
        {
            dynamic record;
            record = _baseDL.GetRecordById(recordId);
            return record;
        }

        /// <summary>
        /// Hàm thêm mới nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public ServiceResult InsertRecord(T record)
        {
            return HandleInsertOrUpdate(Guid.Empty, record);
        }

        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn sửa</param>
        /// <param name="record">bản ghi muốn sửa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        public ServiceResult UpdateRecord(Guid recordId, T record)
        {
            return HandleInsertOrUpdate(recordId, record);
        }

        /// <summary>
        /// Hàm xử lý thêm hoặc sửa nhân viên
        /// </summary>
        /// <param name="recordId">Id của nhân viên muốn sửa</param>
        /// <param name="record">Thông tin nhân viên muốn sửa hoặc thêm</param>
        /// <returns></returns>
        public ServiceResult HandleInsertOrUpdate(Guid recordId, T record)
        {
            ServiceResult validateResults = new ServiceResult();
            validateResults = ValidateData(record);
            // Thất bại -- return lỗi

            if (!validateResults.IsSuccess && (validateResults.ErrorCode == ErrorCode.IsValidData || validateResults.ErrorCode == ErrorCode.DuplicateCode))
            {
                return validateResults;
            }
            var numberOfAffectedResult = 0;
            if (recordId == Guid.Empty)
            {
                numberOfAffectedResult = _baseDL.InsertRecord(record);
            }
            else
            {
                numberOfAffectedResult = _baseDL.UpdateRecord(recordId, record); 
            }
            if (numberOfAffectedResult > 0)
            {
                return new ServiceResult
                {
                    IsSuccess = true,
                };
            }
            else
            {
                //Kết quả trả về
                return new ServiceResult
                {
                    IsSuccess = false,
                    ErrorCode = Common.Enums.ErrorCode.NoData,
                    Message = "Lỗi khi gọi vào DL",
                };
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn xóa</param>
        /// <returns>
        /// 1: Nếu insert thành công
        /// 0: Nếu insert thất bại
        /// </returns>
        /// Created by: NVTan (09/02/2023)
        public int DeleteRecord([FromRoute] Guid recordId)
        {
            var result = _baseDL.DeleteRecord(recordId);
            return result;
        }

        /// <summary>
        /// Hàm validate Required
        /// </summary>
        /// <param name="record"></param>
        /// <param name="lstValidate">Danh sách lỗi</param>
        protected virtual ServiceResult ValidateData(T record)
        {
            //Lấy toàn bộ property của class employee
            var properties = typeof(T).GetProperties();

            //Kiểm tra xem property nào có attribute là riquired
            ServiceResult lstValidate = new ServiceResult();

            lstValidate.IsSuccess = true;
            List<string> lstEmpty = new List<string>();
            lstEmpty.Clear();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(record);
                var requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                var emailAttribute = (EmailAddressAttribute)property.GetCustomAttributes(typeof(EmailAddressAttribute), false).FirstOrDefault();
                var stringLengthAttribute = (StringLengthAttribute?)property.GetCustomAttributes(typeof(StringLengthAttribute), false).FirstOrDefault();
                if (requiredAttribute != null && propertyValue == null)
                {
                    lstEmpty.Add(requiredAttribute.ErrorMessage);
                }
                else if (requiredAttribute != null && string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    lstEmpty.Add(requiredAttribute.ErrorMessage);
                }
                if (propertyValue != null && !string.IsNullOrEmpty(propertyValue.ToString()))
                {
                    if (emailAttribute != null && !IsValidEmail(propertyValue.ToString()))
                    {
                        lstEmpty.Add(Resource.FormatEmail);
                    }
                    /*DateTime datetime;
                    if (DateTime.TryParse(propertyValue.ToString(), out datetime))
                    {
                        DateTime time = DateTime.Parse(propertyValue.ToString());
                        if (time > DateTime.Now)
                        {
                            lstEmpty.Add(Resource.FormatDate);
                        }
                    }*/
                    if (stringLengthAttribute != null && propertyValue.ToString()?.Length > stringLengthAttribute.MaximumLength)
                    {
                        lstEmpty.Add(stringLengthAttribute.ErrorMessage);
                    }
                }
            }
            if (lstEmpty.Count == 0)
            {
                var result = ValidateCustom(record);
                if (result != null)
                {
                    lstValidate.IsSuccess = false;
                    lstValidate.ErrorCode = result.ErrorCode;
                    lstValidate.Data = result.Data;
                }
            }
            if (lstEmpty.Count > 0)
            {
                lstValidate.IsSuccess = false;
                lstValidate.ErrorCode = ErrorCode.IsValidData;
                lstValidate.Data = lstEmpty;
            }

            return lstValidate;
        }

        /// <summary>
        /// Hàm validate Custom
        /// </summary>
        /// <param name="record">Bản ghi muốn kiểm tra validate</param>
        /// <returns>ServiceResult</returns>
        protected virtual ServiceResult ValidateCustom(T? record)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// Kiểm tra validate Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>
        /// true: đúng
        /// false: sai
        /// </returns>
        private static bool IsValidEmail(string email)
        {
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            return Regex.IsMatch(email, regex);
        }

        /// <summary>
        /// Lấy danh sách record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: NVTan (09/02/2023)
        public List<T> GetAllRecord()
        {
            List<T> listRecords;
            listRecords = _baseDL.GetAllRecord();

            //Kết quả trả về
            return listRecords;
        }

        #endregion
    }
}
