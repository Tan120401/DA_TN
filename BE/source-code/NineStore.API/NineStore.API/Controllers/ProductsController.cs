﻿using Microsoft.AspNetCore.Http;
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
        
        /// <summary>
        /// Lấy tất cả sản phẩm 
        /// </summary>
        /// <param name="categoryId">Id danh mục</param>
        /// <param name="order">Sắp xếp theo</param>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns></returns>
        [HttpGet("get-product")]
        public IActionResult GetAllProductByCategory([FromQuery] Guid? categoryId, [FromQuery] string? order, [FromQuery] string? keyword)
        {
            List<Product> products = new List<Product>();
            products = _productBL.GetAllProduct(categoryId, order,keyword);
            if (products != null)
            {
                return StatusCode(StatusCodes.Status201Created, products);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Hàm upload ảnh sản phẩm
        /// </summary>
        /// <param name="fileModel"></param>
        /// <returns></returns>
        [HttpPost("upload-file")]
        public IActionResult CreateFile([FromForm] FileModel fileModel)
        {
            string path = Path.Combine(@"D:\DO_AN\FE\app\src\assets\img\product", fileModel.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                fileModel.File.CopyTo(stream);
            }
            return StatusCode(StatusCodes.Status200OK, 1);
        }

        #endregion
    }
}
