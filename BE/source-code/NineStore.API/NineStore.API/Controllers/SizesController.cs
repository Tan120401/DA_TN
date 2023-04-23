using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.SizeBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    
    public class SizesController : BasesController<Size>
    {
        #region Field

        private ISizeBL _sizeBL;

        #endregion
        public SizesController(ISizeBL sizeBL) : base(sizeBL)
        {
            _sizeBL = sizeBL;
        }
    }
}
