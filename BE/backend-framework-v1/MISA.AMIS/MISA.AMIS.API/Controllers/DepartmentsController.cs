using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Enums;
using MySqlConnector;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;
using System.ComponentModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using MISA.AMIS.Common.Resources;
using System.Drawing;
using System.Reflection;
using MISA.AMIS.DepartmentBL;

namespace MISA.AMIS.API.Controllers
{
    public class DepartmentsController : BasesController<Department>
    {
        #region Field

        private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            _departmentBL = departmentBL;
        }

        #endregion

    }
}
