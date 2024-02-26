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
        private void TasksView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TasksViewModel viewModel)
            {
                viewModel.OnTogglePopupRequested += TogglePopupVisibility;
            }
        }

        private void TogglePopupVisibility(object sender, EventArgs e)
        {
            // Toggle the visibility of the popupBorder
            popupBorder.Visibility = popupBorder.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            popupBorder.Visibility = Visibility.Collapsed;
        }
    }
}
