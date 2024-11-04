using System;

namespace _4roomforum.DTOs;

public class CategoryDTO
{

    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }

}

