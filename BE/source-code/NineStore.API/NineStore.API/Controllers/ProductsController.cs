using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.BaseBL;
using NineStore.BL.CategoryBL;
using NineStore.BL.ProductBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    public class ProductsController : BasesController<Product>
    {
        #region Field

        private IProductBL _productBL;

        #endregion
        public ProductsController(IProductBL productBL) : base(productBL)
        {
            _productBL = productBL;
        }

        #region Method
        [HttpGet("get-product")]
        public IActionResult GetAllProductByCategory([FromQuery] Guid? categoryId, [FromQuery] string? order)
        {
            List<Product> products = new List<Product>();
            products = _productBL.GetAllProduct(categoryId, order);
            if (products != null)
            {
                return StatusCode(StatusCodes.Status201Created, products);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion
    }
}
