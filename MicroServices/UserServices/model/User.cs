using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class User
{
    [Key]
    [Required]
    public int UserId { get; set; }
    [Required]
    public string UserName { get; set; }
    public string Email { get; set; }
     public string PassWord { get; set; }

    public string Avatar { get; set; }
    
    public int RoleId { get; set; }
    public DateOnly JoinDate { get; set; }
    public DateOnly LastLogin { get; set; }
    public int Status { get; set; }

    public override string ToString()
    {
        return "User [UserId=" + UserId + ", UserName=" + UserName + ", Email=" + Email + ", PassWord=" + PassWord + 
            ", Avatar=" + Avatar + ", RoleId=" + RoleId + ", JoinDate=" + JoinDate + 
            ", LastLogin=" + LastLogin + ", Status=" + Status + "]";
    }

}
