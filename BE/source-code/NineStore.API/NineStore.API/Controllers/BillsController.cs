using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.BillBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    
    public class BillsController : BasesController<Bill>
    {
        #region Field

        private IBillBL _billBL;

        #endregion
        public BillsController(IBillBL billBL) : base(billBL)
        {
            _billBL = billBL;
        }
    }
}
