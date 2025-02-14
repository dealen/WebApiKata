using System.Data;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Shared.Interfaces;
using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.RequestModels;

namespace ToDoApi.Repository;

public class ToDoRepository : IToDoRepository
{
    private readonly ToDoContext _toDoContext;

    public ToDoRepository(ToDoContext toDoContext)
    {
        _toDoContext = toDoContext;
    }

    public async Task<ToDoItem> AddAsync(ToDoAddRequest requestItem)
    {
        var item = new ToDoItem() { Name = requestItem.Name, IsCompleted = false };
        var result = await _toDoContext.ToDoItems.AddAsync(item);

        await _toDoContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<ToDoItem> UpdateAsync(ToDoUpdateRequest requestItem)
    {
        var updateItem = _toDoContext.ToDoItems.FirstOrDefault(x => x.Id == requestItem.Id);
        if (updateItem is null)
        {
            throw new DataException("Item not found");
        }

        updateItem.Name = requestItem.Name;
        updateItem.IsCompleted = requestItem.IsCompleted;

        var result = _toDoContext.ToDoItems.Update(updateItem);
        await _toDoContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var itemForDeletion = _toDoContext.ToDoItems.FirstOrDefault(x => x.Id == id);
        if (itemForDeletion is null)
        {
            throw new DataException("Item not found");
        }

        _toDoContext.ToDoItems.Remove(itemForDeletion);
        await _toDoContext.SaveChangesAsync();
    }

    public async Task<ToDoItem> GetByIdAsync(int id)
    {
        var iten = await _toDoContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == id) ?? throw new DataException("Item not found");

        return iten;
    }

    public async Task<List<ToDoItem>> GetAllAsync()
    {
        return await _toDoContext.ToDoItems.ToListAsync();
    }
}