using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskMangementSystem.Models;

namespace TaskMangementSystem.ViewModels
{
    public class TaskViewModel : Screen, INotifyPropertyChanged
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

        public TaskModel.TaskStatus Status
        {
            get { return _task.Status; }
            set
            {
                _task.Status = value;
                NotifyOfPropertyChange(nameof(Status));
                NotifyOfPropertyChange(nameof(Task));
            }
        }

    }
}
