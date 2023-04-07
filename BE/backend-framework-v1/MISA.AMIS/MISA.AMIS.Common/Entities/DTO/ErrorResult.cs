using MISA.AMIS.Common.Enums;
using static MISA.AMIS.Common.Enums.ErrorCode; 

namespace MISA.AMIS.Common.Entities.DTO
{
    /// <summary>
    /// Dữ liệu trả về khi lỗi API
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Message lỗi cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message lỗi cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin về lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// TraceId lỗi
        /// </summary>
        public string TraceId { get; set; }
    }
}