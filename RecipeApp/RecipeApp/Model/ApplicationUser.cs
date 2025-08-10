using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RecipeApp.Model;

public class ApplicationUser : IdentityUser
{
    [Key]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}