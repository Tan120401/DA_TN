using NineStore.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineStore.Common.Entities
{
    public class UserResponse
    {
        public Guid? UserId { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public Role? Role { get; set; }

        public string? Token { get; set; }


        public UserResponse(UserRequest request, string token)
        {
            UserId = request.UserId;
            UserName = request.UserName;
            PassWord = request.PassWord;
            FullName = request.FullName;
            Email = request.Email;
            Role = request.Role;
            Token = token;
        }
        /* public string RefreshToken { get; set; } = string.Empty;
         public DateTime TokenCreated { get; set; }
         public DateTime TokenExpires { get; set; }*/
    }
}
