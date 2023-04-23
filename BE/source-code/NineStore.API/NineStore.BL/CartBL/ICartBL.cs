using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.CartBL
{
    public interface ICartBL : IBaseBL<Cart>
    {
        public List<CartResponse> GetCartByUser(Guid? userId);
    }
}
