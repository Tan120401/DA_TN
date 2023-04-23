using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class Category
    {
        /// <summary>
        /// Id danh mục
        /// </summary>
        [Key]
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Tên danh mục
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
