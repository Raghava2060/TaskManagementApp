using TaskManagementApp.DTOs;
using TaskManagementApp.Models;
using TaskManagementApp.Repositories.Interfaces;

namespace TaskManagementApp.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementContext _context;

        public TaskRepository(TaskManagementContext context)
        {
            _context = context;
        }

        public IEnumerable<TaskDto> GetAll()
        {
            return _context.Tasks
                .Select(t => new TaskDto
                {
                    TaskId = t.TaskId,
                    TaskDescription = t.TaskDescription,
                    StartDate = t.StartDate,
                    ExpectedClosureDate = t.ExpectedClosureDate,
                    AssignedTo = t.AssignedTo,
                    CompletionStatus = t.CompletionStatus
                })
                .ToList();
        }

        public TaskDto? GetById(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task == null)
                return null;

            return new TaskDto
            {
                TaskId = task.TaskId,
                TaskDescription = task.TaskDescription,
                StartDate = task.StartDate,
                ExpectedClosureDate = task.ExpectedClosureDate,
                AssignedTo = task.AssignedTo,
                CompletionStatus = task.CompletionStatus
            };
        }

        public void Add(TaskDto taskDto)
        {
            var task = new Models.Task
            {
                TaskDescription = taskDto.TaskDescription,
                StartDate = taskDto.StartDate,
                ExpectedClosureDate = taskDto.ExpectedClosureDate,
                AssignedTo = taskDto.AssignedTo,
                CompletionStatus = taskDto.CompletionStatus
            };

            _context.Tasks.Add(task);
        }

        public void Update(TaskDto taskDto)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskDto.TaskId);

            if (task != null)
            {
                task.TaskDescription = taskDto.TaskDescription;
                task.StartDate = taskDto.StartDate;
                task.ExpectedClosureDate = taskDto.ExpectedClosureDate;
                task.AssignedTo = taskDto.AssignedTo;
                task.CompletionStatus = taskDto.CompletionStatus;
            }
        }

        public void Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}