using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CatThreadService.DTOs;

public class CategoryDTO
{

    public int CategoryId { get; set; }

    public string CategoryName { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedDate { get; set; }

}
