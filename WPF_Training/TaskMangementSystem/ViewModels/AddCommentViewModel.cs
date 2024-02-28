using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskMangementSystem.Infrastructure;
using TaskMangementSystem.Models;

namespace TaskMangementSystem.ViewModels
{
    public class AddCommentViewModel : Screen
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

        private string _newCommentText;
        public string NewCommentText
        {
            get { return _newCommentText; }
            set
            {
                _newCommentText = value;
                NotifyOfPropertyChange(() => NewCommentText);
            }
        }

        public ICommand AddCommentCommand { get; set; }
        public AddCommentViewModel(TaskModel task)
        {
            Task = task;
            AddCommentCommand = new RelayCommand(AddComment);
        }

        public void AddComment(object obj)
        {
            if (!string.IsNullOrWhiteSpace(NewCommentText))
            {
                // Create a new comment
                var newComment = new CommentModel
                {
                    Id = GenerateCommentId(), // Generate a unique ID for the comment
                    Comment = NewCommentText,
                    DateOfComment = DateTime.Now
                };

                // Add the comment to the task's comments list
                Task.Comments.Add(newComment);

                // Clear the text box after adding the comment
                NewCommentText = string.Empty;
            }
            TryCloseAsync(true);
        }
        private int GenerateCommentId()
        {
            return Task.Comments.Count + 1; 
        }
    }
}
