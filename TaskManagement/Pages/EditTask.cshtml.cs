using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Pages
{
    public class EditTaskModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditTaskModel(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = new TaskItem();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            var task = await _context.Tasks.FindAsync(id);

            if (task == null || task.UserId != userId)
            {
                return NotFound(); // Prevents unauthorized editing
            }
            Task = task;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Find the original task in the database
            var existingTask = await _context.Tasks.FindAsync(Task.Id);

            if (existingTask == null || existingTask.UserId != userId)
            {
                return NotFound(); // Prevent unauthorized updates
            }

            // Update only relevant fields while keeping UserId
            existingTask.Title = Task.Title;
            existingTask.Description = Task.Description;
            existingTask.DueDate = Task.DueDate;
            existingTask.Status = Task.Status; // ✅ Automatically sets completion based on status

            await _context.SaveChangesAsync();

            return RedirectToPage("/TaskList");
        }
    }
}
