﻿using System.ComponentModel.DataAnnotations;

namespace TestingBackend.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; }
    public Role Role { get; set; } = Role.student;
}

public enum Role 
{
    student,
    teacher,
    admin
}