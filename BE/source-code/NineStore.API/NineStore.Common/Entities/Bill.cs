using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class Bill
    {
        [Key]
        public Guid? BillId { get; set; }
        public Guid? OrderId { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public BillCode? IsPay { get; set; }
        public double? SumPrice { get; set; }
    }
}
