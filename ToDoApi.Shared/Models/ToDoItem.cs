using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Shared.Models;

public class ToDoItem
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    public bool IsCompleted { get; set; }
}
