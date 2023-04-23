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

namespace NineStore.BL.SizeBL
{
    
    public class SizeBL : BaseBL<Size>, ISizeBL
    {
        #region Field

        private ISizeDL _sizeDL;

        #endregion
        public SizeBL(ISizeDL sizeDL) : base(sizeDL)
        {
            _sizeDL = sizeDL;
        }

    }
}
