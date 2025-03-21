namespace schiessmeister_csharp.Domain.Models;
using System.ComponentModel.DataAnnotations;

public class RegisterDTO  {
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}