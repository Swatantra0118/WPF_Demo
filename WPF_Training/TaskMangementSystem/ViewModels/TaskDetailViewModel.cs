using Caliburn.Micro;
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
    public class TaskDetailViewModel : Screen,INotifyPropertyChanged
    {
        private TaskViewModel _selectedTaskViewModel;
        private string _newComment;

        public TaskViewModel SelectedTaskViewModel
        {
            get { return _selectedTaskViewModel; }
            set
            {
                _selectedTaskViewModel = value;
                OnPropertyChanged(nameof(SelectedTaskViewModel));
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
            if (SelectedTaskViewModel != null && !string.IsNullOrEmpty(NewComment))
            {
                if (SelectedTaskViewModel.Task.Comments == null)
                    SelectedTaskViewModel.Task.Comments = new List<string>();

                SelectedTaskViewModel.Task.Comments.Add(NewComment);
                SelectedTaskViewModel.NotifyOfPropertyChange(nameof(SelectedTaskViewModel.Task));
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
