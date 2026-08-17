using TaskManagementApp.DTOs;
namespace TaskManagementApp.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<TaskDto> GetAll();

        TaskDto? GetById(int id);

        void Add(TaskDto taskDto);

        void Update(TaskDto taskDto);

        void Delete(int id);

        void Save();
    }
}
