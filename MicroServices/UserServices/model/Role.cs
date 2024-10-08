using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class Role
{
    [Key]
    [Required]
    public int RoleId { get; set; }
    [Required]
    public string RoleName { get; set; }

    public override string ToString()
    {
        return "Role [RoleId=" + RoleId + ", RoleName=" + RoleName + "]";
    }
}
