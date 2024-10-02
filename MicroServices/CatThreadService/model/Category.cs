using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class Category
{
    [Key]
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }

    public override string ToString()
    {
        return "Category [CategoryId=" + CategoryId + ", CategoryName=" + CategoryName + ", Description=" + Description + ", CreatedBy=" + CreatedBy + ", CreatedDate=" + CreatedDate + "]";
    }
}
