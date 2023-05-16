using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class Cart
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
        public Guid? ProductId { get; set;}

        /// <summary>
        /// Size sản phẩm
        /// </summary>
        public int? SizeProduct { get; set; }
        /// <summary>
        /// Số lượng sản phẩm
        /// </summary>
        public int? NumProduct { get; set; }
    }
}
