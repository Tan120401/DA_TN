using NineStore.Common.Entities;
using NineStore.DL.BaseDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.DL.SizeDL
{
    public interface ISizeDL: IBaseDL<Size>
    {
        public int InsertMupltySize(List<Size> Sizes);
        public List<Size> GetSizeByProductId(Guid recordId);

    }
}
