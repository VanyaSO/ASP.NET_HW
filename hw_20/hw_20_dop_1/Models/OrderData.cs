using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace hw_20_dop_1;

[Owned]
public class OrderData
{
    [Required]
    public string? CustomerName { get; set; }
    
    [Required]
    public string? City { get; set; }
    
    [Required]
    public string? Address { get; set; }
}