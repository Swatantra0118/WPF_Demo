using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangementSystem.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public List<CommentModel> Comments { get; set; }
        public enum TaskStatus
        {
            InProgress,
            UnderReview,
            Done
        }
        public TaskStatus Status { get; set; }
    }
}
