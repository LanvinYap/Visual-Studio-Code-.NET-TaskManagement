using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Pages
{
    public class TaskListModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TaskListModel(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                Tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
            }
        }
    }
}
