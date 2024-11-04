using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4roomforum.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Avatar { get; set; }
        
        public int RoleId { get; set; }
        public DateOnly JoinDate { get; set; }
        public DateOnly LastLogin { get; set; }
        public int Status { get; set; }
    }
}