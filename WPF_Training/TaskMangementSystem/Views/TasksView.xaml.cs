using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskMangementSystem.Models;
using TaskMangementSystem.ViewModels;

namespace TaskMangementSystem.Views
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : Window
    {
        public TasksView()
        {
            InitializeComponent();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TaskList.Items.Filter = FilterMethod;


        }

        private bool FilterMethod(object obj)
        {
            if (obj is TaskViewModel taskViewModel)
            {
                // Accessing the TaskModel from the TaskViewModel
                var task = taskViewModel.Task;
                if (task != null)
                {
                    // Performing case-insensitive search on the task name
                    return task.Name.IndexOf(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
            return false;
        }
    }
}
