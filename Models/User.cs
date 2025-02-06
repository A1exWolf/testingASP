using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestingBackend.Models;

public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; } = Role.student;
}

public enum Role 
{
    student,
    teacher,
    admin
}