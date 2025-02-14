using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.RequestModels;

namespace ToDoApi.Shared.Interfaces;

public interface IToDoRepository
{
    Task<List<ToDoItem>> GetAllAsync();
    Task<ToDoItem> GetByIdAsync(int id);
    Task<ToDoItem> AddAsync(ToDoAddRequest requestItem);
    Task<ToDoItem> UpdateAsync(ToDoUpdateRequest requestItem);
    Task DeleteAsync(int id);
}