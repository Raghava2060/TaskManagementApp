using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.DTOs;
using TaskManagementApp.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace TaskManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger<TasksController> _logger;

        public TasksController(
            ITaskRepository repository,
            ILogger<TasksController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: /Tasks
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("User opened the Task List.");
            var tasks = _repository.GetAll();

            _logger.LogInformation("Task list loaded Successfully.");
            return View(tasks);
        }

        // GET: /Tasks/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            _logger.LogInformation("User requested details for Task ID: {TaskId}", id);
            var task = _repository.GetById(id);

            if (task == null)
            {
                _logger.LogWarning("Task ID: {TaskId} was not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Task details loaded successfully for Task ID: {TaskId}", id);

            return View(task);
        }

        // GET: /Tasks/Create
        [HttpGet]
        public IActionResult Create()
        {
            _logger.LogInformation("User opened the Created Task page.");
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskDto taskDto)
        {
            _logger.LogInformation("User submitted a new task.");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Created task failed because the submitted data is invalid.");
                return View(taskDto);
            }

            _repository.Add(taskDto);
            _repository.Save();

            _logger.LogInformation(
                "Task created successfully. Task Description: {TaskDescription}",
                taskDto.TaskDescription);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Tasks/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            _logger.LogInformation("User opened Edit page for Task ID: {TaskId}", id);
            var task = _repository.GetById(id);

            if (task == null)
            {
                _logger.LogWarning("Task ID: {TaskId} was not found for editing.", id);
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
            if (!ModelState.IsValid)
            {
                _logger.LogWarning(
                     "Update failed for Task ID: {TaskId} because the submitted data is invalid.",
                     taskDto.TaskId);

                return View(taskDto);
            }

            _repository.Update(taskDto);
            _repository.Save();

            _logger.LogInformation(
               "Task ID: {TaskId} updated successfully.",
                taskDto.TaskId);
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
            var task = _repository.GetById(id);

            if (task == null)
            {
                _logger.LogWarning(
                    "Task ID: {TaskId} was not found for deletion.",
                    id);
                return NotFound();
            }

            _repository.Delete(id);
            _repository.Save();

            _logger.LogInformation(
                "Task ID: {TaskId} deleted successfully.",
                id);

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