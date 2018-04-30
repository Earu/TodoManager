using ProjectManager.Controls;
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
using TodoManager.Models;

namespace TodoManager.Windows
{
    /// <summary>
    /// Interaction logic for AddProjectWindow.xaml
    /// </summary>
    public partial class AddTodoWindow : Window
    {
        private App _Application;
        private ManagerWindow _Manager;

        internal AddTodoWindow(App app, ManagerWindow win)
        {
            this._Application = app;
            this._Manager = win;
            this.InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this._Manager.Visibility = Visibility.Visible;
            this.Close();
        }

        private void OnDeadlineChecked(object sender, RoutedEventArgs e)
        {
            this.DPDeadline.IsEnabled = true;
        }

        private void OnDeadlineUnchecked(object sender, RoutedEventArgs e)
        {
            this.DPDeadline.IsEnabled = false;
        }

        private void OnOk(object sender, RoutedEventArgs e)
        {
            string name = this.TBName.Text.Trim();
            string desc = this.TBDescription.Text;
            desc = string.IsNullOrWhiteSpace(desc) ? null : desc.Trim();
            DateTime? deadline = null;
            if(this.CBDeadline.IsChecked.HasValue && this.CBDeadline.IsChecked.Value)
                deadline = this.DPDeadline.SelectedDate;

            Todo todo = new Todo(name, desc, deadline);
            this._Application.Todos.Add(todo);
            this._Manager.Visibility = Visibility.Visible;

            TodoControl ctrl = new TodoControl(todo,this._Manager);
            this._Manager.ICTodo.Items.Add(ctrl);

            this.Close();
        }
    }
}
