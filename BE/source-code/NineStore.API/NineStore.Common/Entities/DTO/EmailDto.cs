﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities.DTO
{
    public class EmailDto
    {
        public string? To { get; set; } 
        public string? Subject { get; set; }
        public string? Body { get; set; } 
    }
}
