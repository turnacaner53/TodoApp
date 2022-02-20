using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Entities;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TodoApp;

public class TodoAppService : ApplicationService, ITodoAppService
{
    private readonly IRepository<TodoItem, Guid> todoItemRepository;

    public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository)
    {
        this.todoItemRepository = todoItemRepository;
    }

    public async Task<TodoItemDto> CreateAsync(string text)
    {
        var todoItem = await todoItemRepository.InsertAsync(new TodoItem { Text = text });

        return new TodoItemDto
        {
            Id = todoItem.Id,
            Text = todoItem.Text,
        };
    }

    public async Task DeleteAysnc(Guid id)
    {
        await todoItemRepository.DeleteAsync(id);
    }

    public async Task<List<TodoItemDto>> GetListAsync()
    {
        var items = await todoItemRepository.GetListAsync();
        return items
            .Select(i => new TodoItemDto
            {
                Id = i.Id,
                Text = i.Text
            }).ToList();
    }
}
