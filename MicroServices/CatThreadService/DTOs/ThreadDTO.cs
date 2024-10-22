using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
public class ThreadDTO
{
    public int ThreadId { get; set; }
    public int CategoryID { get; set; }
    public string ThreadTitle { get; set; }
    public  string ThreadContent {  get; set; } 
    public int CreatedBy { get; set; }
    public DateOnly CreadtedDate { get; set; }
    public int ViewCount { get; set; }  
    public int IsPinned { get; set; }
    public int IsClosed { get; set; }
}