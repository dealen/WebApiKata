using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.RequestModels;

namespace ToDoApi.Shared.Interfaces;

public interface IToDoService
{
    Task<List<ToDoItem>> GetAllAsync();
    Task<ToDoItem> GetByIdAsync(int id);
    Task<ToDoItem> CreateAsync(ToDoAddRequest item);
    Task<ToDoItem> UpdateAsync(ToDoUpdateRequest item);
    Task DeleteAsync(int id);
}
