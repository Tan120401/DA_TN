using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.CartDL
{
    public interface ICartDL : IBaseDL<Cart>
    {
        public List<CartResponse> GetCartByUser(Guid? userId);
    }
}
