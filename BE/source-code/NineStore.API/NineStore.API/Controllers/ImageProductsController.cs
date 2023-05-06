using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.ImageProductBL;
using NineStore.BL.SizeBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    
    public class ImageProductsController : BasesController<ImageProduct>
    {
        #region Field

        private IImageProductBL _imageProductBL;

        #endregion
        public ImageProductsController(IImageProductBL imageProductBL) : base(imageProductBL)
        {
            _imageProductBL = imageProductBL;
        }

    }
}
