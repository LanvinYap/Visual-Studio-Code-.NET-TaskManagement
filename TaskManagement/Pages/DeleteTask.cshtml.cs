using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data; // Ensure namespace matches your project
using TaskManagement.Models; // Ensure namespace matches your models

namespace TaskManagement.Pages
{
    public class DeleteTaskModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteTaskModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            Task = task;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Task == null)
            {
                return NotFound();
            }

            var taskToDelete = await _context.Tasks.FindAsync(Task.Id);

            if (taskToDelete != null)
            {
                _context.Tasks.Remove(taskToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/TaskList");
        }
    }
}
