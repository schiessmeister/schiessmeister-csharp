namespace schiessmeister_csharp.Domain.Models;
using System.ComponentModel.DataAnnotations;

public class OrganizerCreateDTO {
    
    [Required(ErrorMessage = "Name is required")]
    public string name { get; set; }
}