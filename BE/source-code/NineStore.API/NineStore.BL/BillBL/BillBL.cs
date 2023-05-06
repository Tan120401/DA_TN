using NineStore.BL.BaseBL;
using NineStore.BL.ProductBL;
using NineStore.Common.Entities;
using NineStore.DL.BillDL;
using NineStore.DL.ProductDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.BillBL
{
    
    public class BillBL : BaseBL<Bill>, IBillBL
    {
        #region Field

        private IBillDL _billDL;

        #endregion
        public BillBL(IBillDL billDL) : base(billDL)
        {
            _billDL = billDL;
        }

    }
}
