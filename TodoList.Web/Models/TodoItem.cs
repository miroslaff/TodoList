using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public bool IsDone { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}