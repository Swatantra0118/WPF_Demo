using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangementSystem.Models;

namespace TaskMangementSystem.ViewModels
{
    public class TaskDetailViewModel : Screen
    {
        private TaskModel _task;
        public TaskModel Task
        {
            get { return _task; }
            set
            {
                _task = value;
                NotifyOfPropertyChange(() => Task);
            }
        }
        public TaskDetailViewModel(TaskModel task)
        {
            Task = task;
        }

        public void SaveTask()
        {
            // Save the modified task details
            // You may implement the logic here
        }
    }
}
