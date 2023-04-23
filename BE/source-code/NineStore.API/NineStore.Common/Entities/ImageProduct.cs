using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class ImageProduct
    {
        [Key]
        public Guid? ImageProductId { get; set; }
        public string? ImageLink { get; set; }
        public Guid? ProductId { get; set; }    
    }
}
