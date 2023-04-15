using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class FileModel
    {
        public string? FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
