using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserServices.DTOs;

public class UserDTO
{

    public int UserId { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string PassWord { get; set; }

    public string Avatar { get; set; }
    
    public int RoleId { get; set; }
    public DateOnly JoinDate { get; set; }
    public DateOnly LastLogin { get; set; }
    public int Status { get; set; }


}
