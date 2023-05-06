using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class Product
    {
        [Key]
        public Guid? ProductId { get; set; }
        public Guid? CategoryId { get; set; }
        public string? ProductName { get; set; }
        public ProductStatus? Status { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public float? Discount { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? ImgProduct { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string?  ModifiedBy { get; set; }
    }
}
