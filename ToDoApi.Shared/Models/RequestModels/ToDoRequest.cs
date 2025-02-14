using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Shared.Models.RequestModels;

public class ToDoAddRequest
{
    [Required]
    public required string Name { get; set; }
}

public class ToDoUpdateRequest
{
    [Required]
    public required int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required bool IsCompleted { get; set; }
}