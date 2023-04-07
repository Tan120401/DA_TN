using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.Common.Entities.DTO;
using MISA.AMIS.DL.BaseDL;
using MISA.AMIS.DL.Datacontext;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL.EmployeeDL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
    }
}
