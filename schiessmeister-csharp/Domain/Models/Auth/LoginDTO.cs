namespace schiessmeister_csharp.Domain.Models.Auth;

using System.ComponentModel.DataAnnotations;

public class LoginDTO {

    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}