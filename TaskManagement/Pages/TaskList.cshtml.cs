using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskMangement.Pages
{
    public class TaskListModel : PageModel
    {
        private readonly AppDbContext _context;

        public TaskListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<TaskItem> Tasks { get; set; } = new();

        public async Task OnGetAsync()
        {
            Tasks = await _context.Tasks.ToListAsync();
        }
    }
}
