using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMangementSystem.Infrastructure;
using TaskMangementSystem.Models;

namespace TaskMangementSystem.ViewModels
{
    public class TaskDetailViewModel : INotifyPropertyChanged
    {
        private TaskModel _selectedTask;
        private string _newComment;

        public TaskModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public string NewComment
        {
            get { return _newComment; }
            set
            {
                _newComment = value;
                OnPropertyChanged(nameof(NewComment));
            }
        }

        public ICommand AddCommentCommand { get; }

        public TaskDetailViewModel()
        {
            AddCommentCommand = new RelayCommand(AddComment);
        }

        private void AddComment(object parameter)
        {
            if (SelectedTask != null && !string.IsNullOrEmpty(NewComment))
            {
                if (SelectedTask.Comments == null) { SelectedTask.Comments = new List<string>(); }
                SelectedTask.Comments.Add(NewComment);
                SelectedTask.Comments.Count();

                NewComment = string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
