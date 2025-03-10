using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public enum TaskStatus
    {
        Pending, // 🕒
        InProgress, // ⏳
        Completed // ✅
    }

    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; } = string.Empty;

        // Change from bool IsCompleted to TaskStatus
        public TaskStatus Status { get; set; } = TaskStatus.Pending; // Default to Pending
    }
}
