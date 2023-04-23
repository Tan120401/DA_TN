using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class Size
    {
        [Key]
        public Guid? SizeId { get; set; }
        public int? SizeNumber { get; set; }
        public Guid? ProductId { get; set; }
    }
}
