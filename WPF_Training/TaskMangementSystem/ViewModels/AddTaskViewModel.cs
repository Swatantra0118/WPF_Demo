using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangementSystem.Models;

namespace TaskMangementSystem.ViewModels
{
    public class AddTaskViewModel : Screen
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

        public AddTaskViewModel()
        {
            Task = new TaskModel();
        }

        public void AddTask()
        {
            TryCloseAsync(true);
        }
    }
}
