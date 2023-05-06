using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities.DTO
{
    public class OrderDetailService
    {
        [Key]
        public Guid? OrderId { get; set; }
        public Guid? OrderDetailId { get; set; }
        public Guid? ProductId { get; set; }
        public OrderStatus? Status { get; set; }
        public string? FullName { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public int? Size { get; set; }
        public string? DisCount { get; set; }
        public string? ImgProduct { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
