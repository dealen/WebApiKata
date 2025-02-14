using ToDoApi.Shared.Interfaces;
using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.RequestModels;

namespace ToDoApi.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _repository;

    public ToDoService(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ToDoItem> CreateAsync(ToDoAddRequest item)
    {
        return await _repository.AddAsync(item);
    }

    public async Task<ToDoItem> UpdateAsync(ToDoUpdateRequest item)
    {
        return await _repository.UpdateAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
         await _repository.DeleteAsync(id);
    }

    public async Task<List<ToDoItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ToDoItem> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
