using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskMangement.Pages
{
    public class AddTaskModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddTaskModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tasks.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("/TaskList");
        }
    }
}
