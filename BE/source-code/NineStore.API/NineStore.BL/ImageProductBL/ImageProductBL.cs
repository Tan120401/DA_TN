using NineStore.BL.BaseBL;
using NineStore.BL.ProductBL;
using NineStore.Common.Entities;
using NineStore.DL.SizeDL;
using NineStore.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NineStore.DL.ImageProductDL;

namespace NineStore.BL.ImageProductBL
{
    
    public class ImageProductBL : BaseBL<ImageProduct>, IImageProductBL
    {
        #region Field

        private IImageProductDL _imageProductDL;

        #endregion
        public ImageProductBL(IImageProductDL imageProductDL) : base(imageProductDL)
        {
            _imageProductDL = imageProductDL;
        }

    }
}
