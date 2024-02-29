using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMangementSystem.Infrastructure;
using TaskMangementSystem.Models;
using TaskMangementSystem.Views;

namespace TaskMangementSystem.ViewModels
{
    public class TasksViewModel : Screen
    {
        private ObservableCollection<TaskViewModel> _tasks;
        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                NotifyOfPropertyChange(nameof(Tasks));
            }
        }

        public ICommand AddTaskCommand { get; set; }
        public ICommand UpdateTaskStatusCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand ShowWindowCommand { get; set; }
        public ICommand ShowTaskDetailsCommand { get; set; }
        public ICommand ShowAddCommentCommand { get; set; }

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<TaskViewModel>();

            // Adding prior tasks
            Tasks.Add(new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now.AddDays(-2), // Example date
                    Name = "Prior Task 1",
                    Description = "Description for Prior Task 1",
                    CreatedBy = "Swatantra",
                    Status = TaskModel.TaskStatus.InProgress,
                    Comments = new List<CommentModel>
                    {
                        new CommentModel
                        {
                            Id = 1,
                            Comment = "First comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-2) // Example date
                        },
                        new CommentModel
                        {
                            Id = 2,
                            Comment = "Second comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-1) // Example date
                        }
                    }
                }
            });

            Tasks.Add(new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now.AddDays(-1), // Example date
                    Name = "Prior Task 2",
                    Description = "Description for Prior Task 2",
                    CreatedBy = "Swatantra",
                    Status = TaskModel.TaskStatus.Triaged,
                    Comments = new List<CommentModel>
                    {
                        new CommentModel
                        {
                            Id = 1,
                            Comment = "First comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-2) // Example date
                        },
                        new CommentModel
                        {
                            Id = 2,
                            Comment = "Second comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-1) // Example date
                        }
                    }
                }
            });

            Tasks.Add(new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now.AddDays(-1), // Example date
                    Name = "Prior Task 2",
                    Description = "Description for Prior Task 2",
                    CreatedBy = "Swatantra",
                    Status = TaskModel.TaskStatus.Done,
                    Comments = new List<CommentModel>
                    {
                        new CommentModel
                        {
                            Id = 1,
                            Comment = "First comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-2) // Example date
                        },
                        new CommentModel
                        {
                            Id = 2,
                            Comment = "Second comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-1) // Example date
                        }
                    }
                }
            });

            Tasks.Add(new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now.AddDays(-1), // Example date
                    Name = "Prior Task 4",
                    Description = "Description for Prior Task 2",
                    CreatedBy = "Swatantra",
                    Status = TaskModel.TaskStatus.UnderReview,
                    Comments = new List<CommentModel>
                    {
                        new CommentModel
                        {
                            Id = 1,
                            Comment = "First comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-2) // Example date
                        },
                        new CommentModel
                        {
                            Id = 2,
                            Comment = "Second comment on the task",
                            DateOfComment = DateTime.Now.AddDays(-1) // Example date
                        }
                    }
                }
            });

            AddTaskCommand = new RelayCommand(AddTask);
            UpdateTaskStatusCommand = new RelayCommand(UpdateTaskStatus);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
            ShowTaskDetailsCommand = new RelayCommand(ShowTaskDetails, CanShowTaskDetails);
            ShowAddCommentCommand = new RelayCommand(showAddComment, CanShowAddComment);
        }

        private bool CanShowAddComment(object obj)
        {
            return true;
        }

        private async void showAddComment(object obj)
        {
            if (obj is TaskModel taskModel)
            {
                var addCommentViewModel = new AddCommentViewModel(taskModel);
                var windowManager = new WindowManager();
                bool? result = await windowManager.ShowDialogAsync(addCommentViewModel);
            }
        }

        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private async void ShowWindow(object obj)
        {
            //AddTask addTaskWindow = new AddTask();
            //addTaskWindow.Show();
            AddTaskViewModel addTaskViewModel = new AddTaskViewModel();
            WindowManager windowManager = new WindowManager();
            bool? result = await windowManager.ShowDialogAsync(addTaskViewModel);

            if (result.HasValue && result.Value)
            {
                // If the user confirmed adding the task
                addTaskViewModel.Task.Id = Guid.NewGuid();
                addTaskViewModel.Task.DateOfCreation = DateTime.Now;
                //addTaskViewModel.Task.Status = TaskModel.TaskStatus.InProgress;
                var newTaskViewModel = new TaskViewModel
                {
                    Task = addTaskViewModel.Task
                };
                Tasks.Add(newTaskViewModel);
            }
        }

        private bool CanShowTaskDetails(object obj)
        {
            return true;
        }

        private async void ShowTaskDetails(object obj)
        {
            if (obj is TaskModel taskModel)
            {
                var taskDetailViewModel = new TaskDetailViewModel(taskModel);
                var windowManager = new WindowManager();
                bool? result = await windowManager.ShowDialogAsync(taskDetailViewModel);
            }
        }

        private void AddTask(object obj)
        {
            // Add new task logic here
            var newTaskViewModel = new TaskViewModel
            {
                Task = new TaskModel
                {
                    Id = Guid.NewGuid(),
                    DateOfCreation = DateTime.Now,
                    Name = "New Task",
                    Description = "This is a new task.",
                    CreatedBy = "Swatantra",
                    Status = TaskModel.TaskStatus.InProgress
                }
            };
            Tasks.Add(newTaskViewModel);
        }

        private void UpdateTaskStatus(object obj)
        {
            // Update task status logic here
            if (obj is TaskViewModel taskViewModel)
            {
                TaskModel.TaskStatus newStatus = taskViewModel.Status;

                taskViewModel.Task.Status = newStatus;
                //NotifyOfPropertyChange(nameof(Tasks));
            }

        }

        private void DeleteTask(object obj)
        {
            if (obj is TaskViewModel taskToDelete)
            {
                Tasks.Remove(taskToDelete);
            }
        }

        public Array TaskStatusValues => Enum.GetValues(typeof(TaskModel.TaskStatus));

        //private void ChangeTaskStatus(object parameter)
        //{
        //    if (parameter is TaskViewModel taskViewModel && parameter is TaskModel.TaskStatus newStatus)
        //    {
        //        taskViewModel.Task.Status = newStatus;
        //    }
        //}
    }
}
