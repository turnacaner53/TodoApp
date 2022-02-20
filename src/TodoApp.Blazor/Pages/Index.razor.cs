using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Blazor.Pages;

public partial class Index
{
    [Inject]
    private ITodoAppService TodoAppService { get; set; }

    private List<TodoItemDto> TodoItems { get; set; } = new List<TodoItemDto>();
    private string NewTodoText { get; set; }

    protected override async Task OnInitializedAsync()
    {
        TodoItems = await TodoAppService.GetListAsync();
    }

    private async Task Create()
    {
        var result = await TodoAppService.CreateAsync(NewTodoText);
        TodoItems.Add(result);
        NewTodoText = null;
    }

    private async Task Delete(TodoItemDto todoItem)
    {
        await TodoAppService.DeleteAysnc(todoItem.Id);
        await Notify.Info("Todo item deleted = " + todoItem.Text);
        TodoItems.Remove(todoItem);
    }
}
