using System;

namespace _4roomforum.DTOs;

public class CategoryDetailDTO
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
    public string Description { get; set; }
    public UserDTO CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }
}
