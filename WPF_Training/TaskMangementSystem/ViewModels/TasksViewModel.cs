using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    DateOfCreation = DateTime.Now.AddDays(-1), // Example date
                    Heading = "Prior Task 2",
                    Description = "Description for Prior Task 2",
                    Status = TaskModel.TaskStatus.InProgress
                }
            });

            AddTaskCommand = new RelayCommand(AddTask);
            UpdateTaskStatusCommand = new RelayCommand(UpdateTaskStatus);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            ShowWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
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
                addTaskViewModel.Task.Status = TaskModel.TaskStatus.InProgress;
                var newTaskViewModel = new TaskViewModel
                {
                    Task = addTaskViewModel.Task
                };
                Tasks.Add(newTaskViewModel);
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

        private void DeleteTask(object obj)
        {
            // Delete task logic here
        }
    }
}
