using System.ComponentModel.DataAnnotations;

namespace TestingBackend.Models;

public class Product
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}