using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Shared.Models;

public class UserModel
{
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
}