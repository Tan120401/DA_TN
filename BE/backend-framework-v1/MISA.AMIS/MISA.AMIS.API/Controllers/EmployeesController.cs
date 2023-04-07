using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.Common.Enums;
using MISA.AMIS.DL.BaseDL;
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
using MISA.AMIS.EmployeeBL;

namespace MISA.AMIS.API.Controllers
{
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

    }
}
