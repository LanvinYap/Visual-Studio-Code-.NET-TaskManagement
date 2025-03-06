using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Pages
{
    public class AddTaskModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddTaskModel(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public TaskItem Task { get; set; } = new TaskItem();

        public IActionResult OnPost()
        {
            var user = _userManager.GetUserId(User); // Get the logged-in user's ID
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Task.UserId = user; // Assign the task to the logged-in user

            _context.Tasks.Add(Task);
            _context.SaveChanges();

            return RedirectToPage("/TaskList");
        }
    }
}
