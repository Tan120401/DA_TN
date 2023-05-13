using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Other
{
    public class PaymentInput
    {
        public Guid? OrderId { get; set; }
        public string? Amount { get; set; }
    }
}
