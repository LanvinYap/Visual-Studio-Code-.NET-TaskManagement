using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime DueDate { get; set; } = DateTime.UtcNow;
    }
}
