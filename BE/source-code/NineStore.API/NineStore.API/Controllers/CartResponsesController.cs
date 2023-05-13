using NineStore.BL.BaseBL;
using NineStore.BL.CartBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    public class CartResponsesController : BasesController<CartResponse>
    {
        #region Field

        public CartResponsesController(IBaseBL<CartResponse> baseBL) : base(baseBL)
        {
        }

        #endregion

    }
}
