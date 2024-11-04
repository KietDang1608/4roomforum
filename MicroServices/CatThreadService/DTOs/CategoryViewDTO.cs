using System;

namespace CatThreadService.DTOs;

public class CategoryViewDTO
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }

    public int ViewCount { get; set; }
}
