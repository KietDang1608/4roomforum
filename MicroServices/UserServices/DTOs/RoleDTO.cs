using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserServices.DTOs;

public class RoleDTO
{

    public int RoleId { get; set; }

    public string RoleName { get; set; }

}
