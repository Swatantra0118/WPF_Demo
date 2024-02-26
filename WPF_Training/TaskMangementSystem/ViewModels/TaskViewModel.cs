using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskMangementSystem.Models;

namespace TaskMangementSystem.ViewModels
{
    public class TaskViewModel : Screen
    {
        private TaskModel _task;
        public TaskModel Task
        {
            get { return _task; }
            set
            {
                _task = value;
                NotifyOfPropertyChange(nameof(Task));
            }
        }

    }
}
