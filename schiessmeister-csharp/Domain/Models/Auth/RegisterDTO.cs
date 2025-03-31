using System.ComponentModel.DataAnnotations;

namespace schiessmeister_csharp.Domain.Models.Auth;

public class RegisterDTO {

    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}