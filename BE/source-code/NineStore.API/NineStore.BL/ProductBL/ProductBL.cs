using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using NineStore.DL.CategoryDL;
using NineStore.DL.ProductDL;
using NineStore.DL.UserDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.ProductBL
{
    public class ProductBL : BaseBL<Product>, IProductBL
    {
        #region Field

        private IProductDL _productDL;

        #endregion
        public ProductBL(IProductDL productDL) : base(productDL)
        {
            _productDL = productDL;
        }

        public List<Product> GetAllProduct(Guid? categoryId, string? order)
        {
            List<Product> products= new List<Product>();
            products = _productDL.GetAllProduct(categoryId, order);
            return products;
        }
    }
}
