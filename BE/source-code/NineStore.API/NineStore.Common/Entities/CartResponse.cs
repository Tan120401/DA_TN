using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class CartResponse
    {
        /// <summary>
        /// Id giỏ hàng
        /// </summary>
        [Key]
        public Guid? CartId { get; set; }
        /// <summary>
        /// Id người dùng
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Id sản phẩm
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Size sản phẩm
        /// </summary>
        public int? SizeProduct { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public int? NumProduct { get; set; }
        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public int? Price { get; set; }
        /// <summary>
        /// Số lượng còn lại
        /// </summary>
        public int? Quantity { get; set; }
        /// <summary>
        /// Giảm giả
        /// </summary>
        public int? Discount { get; set; }
        /// <summary>
        /// Ảnh sản phẩm
        /// </summary>
        public string? ImgProduct { get; set; }
        
      
        
        
    }
}
