using NineStore.BL.BaseBL;
using NineStore.BL.ProductBL;
using NineStore.Common.Entities;
using NineStore.DL.CartDL;
using NineStore.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.CartBL
{
    
    public class CartBL : BaseBL<Cart>, ICartBL
    {
        #region Field

        private ICartDL _cartDL;

        #endregion
        public CartBL(ICartDL cartDL) : base(cartDL)
        {
            _cartDL = cartDL;
        }

        public List<CartResponse> GetCartByUser(Guid? userId)
        {
            List<CartResponse> results = _cartDL.GetCartByUser(userId);
            return results;
        }
    }
}
