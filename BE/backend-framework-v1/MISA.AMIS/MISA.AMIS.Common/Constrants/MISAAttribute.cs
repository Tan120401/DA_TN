using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Constrants
{
    public class MISAAttribute
    {
        public class NotEmpty : Attribute
        {
            #region Field
            public string ErrorMessage = string.Empty;
            #endregion

            #region Contractor
            public NotEmpty(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
            #endregion
        }

        public class Email : Attribute
        {
            #region Field
            public string ErrorMessage = string.Empty;
            #endregion

            #region Contractor
            public Email(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
            #endregion
        }

        public class Date : Attribute
        {
            #region Field
            public string ErrorMessage = string.Empty;
            #endregion

            #region Contractor
            public Date(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
            #endregion
        }
        public class StringLengthAttribute : Attribute
        {
            public string ErrorMessage = string.Empty;
            public int MaxLength;
            
            public StringLengthAttribute(int maxLength, string errorMessage)
            {
                ErrorMessage = errorMessage;
                MaxLength = maxLength;
            }
        }
    }
}
