using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Pages
{
    public class EditTaskModel : PageModel
    {
        public readonly AppDbContext _context;

        public EditTaskModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = new();

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToPage("/TaskList");
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
