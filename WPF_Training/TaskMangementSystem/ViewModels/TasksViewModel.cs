using Caliburn.Micro;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TaskMangementSystem.Infrastructure;
using TaskMangementSystem.Models;
using TaskMangementSystem.Views;

namespace TaskMangementSystem.ViewModels
{
    public class TasksViewModel : Screen, INotifyPropertyChanged
    {
        private TaskViewModel _selectedTask;
        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                if (_selectedTask != value)
                {
                    _selectedTask = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPopupVisible)); 
                    FilterComments();
                }
            }
        }

        public bool IsPopupVisible => SelectedTask != null;

        private ObservableCollection<TaskViewModel> _tasks;
        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CommentModel> _comments;
        public ObservableCollection<CommentModel> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }
        private string _commentText;
        public string CommentText
        {
            get { return _commentText; }
            set
            {
                _commentText = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddTaskCommand { get; set; }
        public ICommand UpdateTaskStatusCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand AddCommentCommand { get; set; }
        public ICommand ViewpopCommand { get; set; }

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<TaskViewModel>();
            Comments = new ObservableCollection<CommentModel>();

            // Adding prior tasks
            Tasks.Add(new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now.AddDays(-2),
                    Heading = "Prior Task 1",
                    Description = "Description for Prior Task 1",
                    Status = TaskModel.TaskStatus.InProgress
                }
            });

            Tasks.Add(new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now.AddDays(-1),
                    Heading = "Prior Task 2",
                    Description = "Description for Prior Task 2",
                    Status = TaskModel.TaskStatus.InProgress
                }
            });

            AddTaskCommand = new RelayCommand(AddTask);
            UpdateTaskStatusCommand = new RelayCommand(UpdateTaskStatus);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            AddCommentCommand = new RelayCommand(AddComment);
            ViewpopCommand = new RelayCommand(Viewpop);
        }

        private void Viewpop(object obj)
        {
            // Toggle the visibility of the popup by raising an event
            OnTogglePopupRequested?.Invoke(this, EventArgs.Empty);
        }

        // Event to request toggling the popup visibility
        public event EventHandler OnTogglePopupRequested;

        private void AddTask(object obj)
        {
            // Add new task logic here
            var newTaskViewModel = new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now,
                    Heading = "New Task",
                    Description = "This is a new task.",
                    Status = TaskModel.TaskStatus.InProgress
                }
            };
            Tasks.Add(newTaskViewModel);
        }

        private void UpdateTaskStatus(object obj)
        {
            // Update task status logic here
        }

        private void DeleteTask(object parameter)
        {
            if (parameter is TaskViewModel taskToDelete)
            {
                Tasks.Remove(taskToDelete);
            }
        }
        private void AddComment(object parameter)
        {
            //if (!string.IsNullOrWhiteSpace(CommentText) && SelectedTask != null)
            //{
            //    if (SelectedTask.Task.Comments == null)
            //    {
            //        SelectedTask.Task.Comments = new ObservableCollection<CommentModel>();
            //    }

            //    SelectedTask.Task.Comments.Add(new CommentModel
            //    {
            //        Id = SelectedTask.Task.Comments.Count + 1,
            //        Comment = CommentText,
            //        DateOfComment = DateTime.Now
            //    });

            //    CommentText = "";
            //}

            // Check if the comment text is not empty
            if (!string.IsNullOrWhiteSpace(CommentText))
            {
                // Add a new comment with the entered text
                Comments.Add(new CommentModel
                {
                    Id = Comments.Count + 1,
                    Comment = CommentText,
                    DateOfComment = DateTime.Now
                });

                CommentText = "";
            }
        }

        private void FilterComments()
        {
            if (SelectedTask != null)
            {
                if (SelectedTask.Task.Comments != null && SelectedTask.Task.Comments.Any())
                {
                    
                    Comments = new ObservableCollection<CommentModel>(SelectedTask.Task.Comments);
                }
                else
                {
                    Comments.Clear(); 
                }
            }
            else
            {
                Comments.Clear(); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
