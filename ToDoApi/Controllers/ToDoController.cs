using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Shared.Interfaces;
using ToDoApi.Shared.Models;
using ToDoApi.Shared.Models.RequestModels;

namespace ToDoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;
        private readonly IToDoService _toDoService;

        public ToDoController(
            ILogger<ToDoController> logger,
            IToDoService toDoService)
        {
            _logger = logger;
            _toDoService = toDoService;

            _logger.LogInformation("ToDoController created");
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ToDoItem>> Get()
        {
            return await _toDoService.GetAllAsync();
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<ToDoItem> GetById(int id)
        {
            return await _toDoService.GetByIdAsync(id);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ToDoItem> Create(ToDoAddRequest item)
        {
            return await _toDoService.CreateAsync(item);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ToDoItem> Update(ToDoUpdateRequest item)
        {
            return await _toDoService.UpdateAsync(item);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task Delete(int id)
        {
            await _toDoService.DeleteAsync(id);
        }
    }
}