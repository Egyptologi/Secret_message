namespace Exam_asp.Models;

using System.ComponentModel.DataAnnotations;

public class SecretMessage
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Message { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
}