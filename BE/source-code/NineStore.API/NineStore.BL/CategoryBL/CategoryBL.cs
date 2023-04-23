using NineStore.BL.BaseBL;
using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using NineStore.DL.CategoryDL;
using NineStore.DL.UserDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.BL.CategoryBL
{
    public class CategoryBL : BaseBL<Category>, ICategoryBL
    {
        #region Field

        private ICategoryDL _categoryDL;

        #endregion
        public CategoryBL(ICategoryDL categoryDL) : base(categoryDL)
        {
            _categoryDL = categoryDL;
        }
    }
}
