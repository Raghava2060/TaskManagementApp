using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.DTOs;
using TaskManagementApp.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using TaskManagementApp.Logging;

namespace TaskManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<TasksController> _logger;

        private readonly ICustomLogger _customLogger;

        public TasksController(
            ITaskRepository repository,
            ILogger<TasksController> logger,
            ICustomLogger customLogger)
        {
            _repository = repository;
            _logger = logger;
            _customLogger = customLogger;
        }

        // GET: /Tasks
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("User opened the Task List.");
            _customLogger.LogInformation("User opened the Task List.");

            var tasks = _repository.GetAll();

            _logger.LogInformation("Task list loaded Successfully.");
            _customLogger.LogInformation("Task list loaded successfully.");
            return View(tasks);
        }

        // GET: /Tasks/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            _logger.LogInformation("User requested details for Task ID: {TaskId}", id);
            _customLogger.LogInformation(
                $"User requested details for Task ID: {id}");

            var task = _repository.GetById(id);

            if (task == null)
            {
                _logger.LogWarning("Task ID: {TaskId} was not found.", id);
                _customLogger.LogWarning($"Task ID: {id} was not found.");
                return NotFound();
            }
            
            _customLogger.LogInformation($"Task details loaded successfully for Task ID: {id}");

            return View(task);
        }

        // GET: /Tasks/Create
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("User opened the Created Task page.");
            _customLogger.LogInformation(
                "User opened the Create Task page.");
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskDto taskDto)
        {
            _logger.LogInformation("User submitted a new task.");
            _customLogger.LogInformation(
                "User submitted a new task.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Created task failed because the submitted data is invalid.");
                _customLogger.LogWarning(
                    "Created task failed because the submitted data is invalid.");
                return View(taskDto);
            }

            _repository.Add(taskDto);
            _repository.Save();

            _logger.LogInformation(
                "Task created successfully. Task Description: {TaskDescription}",
                taskDto.TaskDescription);

            _customLogger.LogInformation(
                $"Task created successfully. Task Description: {taskDto.TaskDescription}");

            return RedirectToAction(nameof(Index));
        }

        // GET: /Tasks/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation("User opened Edit page for Task ID: {TaskId}", id);
            var task = _repository.GetById(id);

            _customLogger.LogInformation(
                $"User opened Edit page for Task ID: {id}");
            if (task == null)
            {
                _logger.LogWarning("Task ID: {TaskId} was not found for editing.", id);
                _customLogger.LogWarning($"Task ID: {id} was not found for editing.");
                return NotFound();
            }

            return View(task);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskDto taskDto)
        {
            _logger.LogInformation(
                "User submitted an update for Task ID: {TaskId}",
                  taskDto.TaskId);
            _customLogger.LogInformation(
                $"User submitted an update for Task ID: {taskDto.TaskId}");
            if (!ModelState.IsValid)
            {
                _customLogger.LogWarning(
            $"Update failed for Task ID: {taskDto.TaskId} because the submitted data is invalid.");

                return View(taskDto);
            }

            _repository.Update(taskDto);
            _repository.Save();

            _customLogger.LogInformation(
               "Task ID: {taskDtoTaskId} updated successfully.");
            return RedirectToAction(nameof(Index));
        }

        // DELETE: /Tasks/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation(
                 "User requested deletion of Task ID: {TaskId}",
                  id);
            _customLogger.LogInformation(
               $"User requested deletion of Task ID: {id}");
            var task = _repository.GetById(id);

            if (task == null)
            {
                _logger.LogWarning(
                    "Task ID: {TaskId} was not found for deletion.", id);
                _customLogger.LogWarning($"Task ID: {id} was not found for deletion.");
                return NotFound();
            }

            _repository.Delete(id);
            _repository.Save();

            _customLogger.LogInformation(
                $"Task ID: {id} deleted successfully.");

            return RedirectToAction(nameof(Index));
        }

        // PUT: /Tasks/Update
        [HttpPut]
        public IActionResult Update(TaskDto taskDto)
        {
            var existingTask = _repository.GetById(taskDto.TaskId);

            if (existingTask == null)
            {
                return NotFound();
            }

            _repository.Update(taskDto);
            _repository.Save();

            return Ok(taskDto);
        }

        // PATCH: /Tasks/UpdateStatus/5
        [HttpPatch]
        public IActionResult UpdateStatus(int id, string completionStatus)
        {
            var task = _repository.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            task.CompletionStatus = completionStatus;

            _repository.Update(task);
            _repository.Save();

            return Ok(task);
        }
    }
}