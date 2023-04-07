using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL.BaseBL;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.Datacontext;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.EmployeeBL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        
    }
}
