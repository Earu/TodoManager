using TodoManager.Controls;
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
        private ManagerWindow _Manager;
        private bool _IsNewTodo;
        private Todo _Todo;

        internal AddTodoWindow(ManagerWindow win)
        {
            this._Manager     = win;
            this._IsNewTodo   = true;
            this._Todo        = null;
            this.InitializeComponent();
        }

        internal AddTodoWindow(ManagerWindow win, Todo todo)
        {
            this._Manager = win;
            this._IsNewTodo = false;
            this._Todo = todo;
            this.InitializeComponent();
            this.TBName.Text = todo.Name;
            this.TBDescription.Text = todo.Description;
            if (todo.Deadline.HasValue)
            {
                this.CBDeadline.IsChecked = true;
                this.DPDeadline.SelectedDate = todo.Deadline.Value;
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this._Manager.Visibility = Visibility.Visible;
            this._Manager.CanClose = true;
            this._Manager.Activate();
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
            if (this.CBDeadline.IsChecked.HasValue && this.CBDeadline.IsChecked.Value)
                deadline = this.DPDeadline.SelectedDate;

            this._Manager.Visibility = Visibility.Visible;

            if (this._IsNewTodo)
            {
                Todo todo = new Todo(name, desc, deadline);
                this._Manager.Application.Todos.Add(todo);

                TodoControl ctrl = new TodoControl(todo, this._Manager);
                this._Manager.ICTodo.Items.Add(ctrl);
            }
            else
            {
                Todo todo        = this._Manager.Application.Todos.First(x => x.ID == this._Todo.ID);
                todo.Name        = name;
                todo.Description = desc;
                todo.Deadline    = deadline;

                TodoControl ctrl = this._Manager.SelectedControl;
                ctrl.TBName.Text = name;
            }

            this.Close();
        }
    }
}
