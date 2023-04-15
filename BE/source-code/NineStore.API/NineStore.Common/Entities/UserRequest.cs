using Microsoft.AspNetCore.Http;
using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class UserRequest
    {
        [Key]
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? ImgName { get; set; }
    }
}
