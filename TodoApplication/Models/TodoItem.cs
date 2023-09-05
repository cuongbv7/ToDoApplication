using System;

namespace TodoApplication.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public string Description { get; set; }

        
    }
}
