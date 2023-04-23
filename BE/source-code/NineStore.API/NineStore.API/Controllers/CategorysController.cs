using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NineStore.BL.BaseBL;
using NineStore.BL.CategoryBL;
using NineStore.Common.Entities;

namespace NineStore.API.Controllers
{
    public class CategorysController : BasesController<Category>
    {
        #region Field

        private ICategoryBL _categoryBL;

        #endregion
        public CategorysController(ICategoryBL categoryBL) : base(categoryBL)
        {
            _categoryBL = categoryBL;
        }
    }
}
