

using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } ="";
    
    [Required]
    [MinLength(4)]
    public string Password { get; set; } = "" ;
}