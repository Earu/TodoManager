using ProjectManager.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TodoManager.Models;

namespace TodoManager.Windows
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private App _Application;
        private TodoControl _SelectedControl;

        public ManagerWindow(App app)
        {
            this._Application = app;
            this._SelectedControl = null;
            this.InitializeComponent();
        }

        internal TodoControl SelectedControl { get => this._SelectedControl; set => this._SelectedControl = value; }

        private void OnAddTodo(object sender, RoutedEventArgs e)
        {
            AddTodoWindow win = new AddTodoWindow(this._Application, this)
            {
                Top = this.Top,
                Left = this.Left
            };
            this.Visibility = Visibility.Hidden;
            win.ShowDialog();
        }

        private void UIRemoveSelectedTodo()
        {
            ItemCollection collection = ((ItemsControl)this._SelectedControl.Parent).Items;
            int index = -1;
            for (int i = 0; i < collection.Count; i++)
            {
                TodoControl c = (TodoControl)collection[i];
                if (c.Todo.ID == this._SelectedControl.Todo.ID)
                    index = i;
            }

            if (index != -1)
                collection.RemoveAt(index);
        }

        private void OnRemoveTodo(object sender, RoutedEventArgs e)
        {
            if (this._SelectedControl != null)
            {
                Todo todo = this._SelectedControl.Todo;
                if (todo.State == TodoState.Todo)
                {
                    this.UIRemoveSelectedTodo();
                    todo = this._Application.Todos.FirstOrDefault(x => x.ID == todo.ID);
                    if (todo != null)
                    {
                        this._Application.Todos.Remove(todo);
                        string path = $"Todos/{todo.ID}.proj";
                        if (File.Exists(path))
                            File.Delete(path);
                        this._SelectedControl = null;
                    }
                }
            }
        }

        private void SetSelectedTodoState(TodoState state,ItemsControl ic)
        {
            if (this._SelectedControl != null)
            {
                Todo todo = this._SelectedControl.Todo;
                todo.State = state;

                this.UIRemoveSelectedTodo();
                
                TodoControl ctrl = new TodoControl(todo,this);
                ic.Items.Add(ctrl);
                BrushConverter cvter = new BrushConverter();
                ctrl.RCBackground.Fill = (SolidColorBrush)(cvter.ConvertFrom("#444444"));
                this._SelectedControl = ctrl;
            }
        }

        private void TodoToOnGoing(object sender, RoutedEventArgs e)
        {
            this.SetSelectedTodoState(TodoState.OnGoing, this.ICOnGoing);
        }

        private void OnGoingToTodo(object sender, RoutedEventArgs e)
        {
            this.SetSelectedTodoState(TodoState.Todo, this.ICTodo);
        }

        private void OnGoingToDone(object sender, RoutedEventArgs e)
        {
            this.SetSelectedTodoState(TodoState.Done, this.ICDone);
        }

        private void DoneToOnGoing(object sender, RoutedEventArgs e)
        {
            this.SetSelectedTodoState(TodoState.OnGoing, this.ICOnGoing);
        }

        private void OnClearDone(object sender, RoutedEventArgs e)
        {
            List<Todo> todelete = new List<Todo>();
            foreach(Todo t in this._Application.Todos.Where(x => x.State == TodoState.Done))
            {
                string path = $"Todos/{t.ID}.proj";
                if (File.Exists(path))
                    File.Delete(path);
                todelete.Add(t);
            }

            foreach(Todo t in todelete)
                this._Application.Todos.Remove(t);
            this.ICDone.Items.Clear();
        }
    }
}
