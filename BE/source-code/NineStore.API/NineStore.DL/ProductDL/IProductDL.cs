using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.ProductDL
{
    public interface IProductDL : IBaseDL<Product>
    {
        public List<Product> GetAllProduct(Guid? categoryId, string? order);
    }
}
