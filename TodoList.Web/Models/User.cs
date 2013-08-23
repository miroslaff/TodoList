using System.Collections.Generic;

namespace TodoList.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; }
    }
}