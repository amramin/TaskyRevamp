using System.Linq.Expressions;
using TaskyRevamp.Domain.Models.Task;
using TaskyRevamp.Dto.GeneralDto;

namespace TaskyRevamp.Domain.Interfaces.Repositeries;

public interface ITaskRepository
{
    public Task<List<TaskItem>> GetTasks(PagingParameterModel? paging, Expression<Func<TaskItem, object>> sortBy,
        string sortOrder,
        Expression<Func<TaskItem, bool>>? whereExp = null);
    
    public Task<TaskItem?> GetTaskById(Guid id);
    
    public Task<TaskItem?> UpdateTask(TaskItem task);
    
}