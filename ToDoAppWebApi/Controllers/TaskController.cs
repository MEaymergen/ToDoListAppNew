using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAppWebApi.Models;
using static ToDoAppWebApi.Data.AuthDbContext;

namespace ToDoAppMvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/task/3 ← UserId = 3 olan görevleri getir
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreateDate)
                .ToListAsync();

            return Ok(tasks);
        }

        // POST: api/task ← yeni görev ekle
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/task/5 ← id'si 5 olan görevi güncelle
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.Detail = updatedTask.Detail;
            task.IsCompleted = updatedTask.IsCompleted;
            task.Priority = updatedTask.Priority;

            await _context.SaveChangesAsync();
            return Ok(task);
        }

        // DELETE: api/task/5 ← id'si 5 olan görevi sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }
    }
}
