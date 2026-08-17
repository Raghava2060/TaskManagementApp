using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.DTOs;
using TaskManagementApp.Repositories.Interfaces;

namespace TaskManagementApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        // GET: /Tasks
        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _repository.GetAll();
            return View(tasks);
        }

        // GET: /Tasks/Details/5
        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = _repository.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: /Tasks/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return View(taskDto);
            }

            _repository.Add(taskDto);
            _repository.Save();

            return RedirectToAction(nameof(Index));
        }

        // GET: /Tasks/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _repository.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: /Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return View(taskDto);
            }

            _repository.Update(taskDto);
            _repository.Save();

            return RedirectToAction(nameof(Index));
        }

        // DELETE: /Tasks/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var task = _repository.GetById(id);

            if (task == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            _repository.Save();

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