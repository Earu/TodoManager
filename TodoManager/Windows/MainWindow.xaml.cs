using TodoManager.Utils;
using System.Windows;
using System.Windows.Input;
using TodoManager.Models;
using TodoManager.Controls;
using System.IO;

namespace TodoManager.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ManagerWindow _Manager;
        private App _Application;

        internal MainWindow(App app)
        {
            this._Manager = null;
            this._Application = app;
            this.InitializeComponent();
            this.Topmost = true;
            this.Closing += this.OnClosing;
        }

        internal ManagerWindow Manager { get => this._Manager; set => this._Manager = value; }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!this._Application.TrySaveTodos())
            {
                ExceptionWindow win = new ExceptionWindow();
                win.SetText(@"There was an issue when saving your Todos,
                    you may experience a data loss the next time you open the program.");
            }
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            //This processing is so the manager doesnt pop outside the screen
            if(this._Manager == null)
            {
                System.Drawing.Rectangle bounds = this.GetScreen().Bounds;
                int sw = bounds.Width;
                int sh = bounds.Height;

                ManagerWindow win = new ManagerWindow(this._Application);
                this._Manager = win;
                double xoffset = this.Left + this.Width / 2;
                double yoffset = this.Top + this.Height / 2;
                if (xoffset + win.Width >= sw)
                    xoffset = this.Left - win.Width + this.Width / 2;
                if (yoffset + win.Height >= sh)
                    yoffset = this.Top - win.Height + this.Height / 2;
                win.Top = yoffset;
                win.Left = xoffset;
                win.Show();

                foreach (Todo t in this._Application.Todos)
                {
                    TodoControl ctrl = new TodoControl(t,win);
                    switch(t.State)
                    {
                        case TodoState.Todo:
                            win.ICTodo.Items.Add(ctrl);
                            break;
                        case TodoState.OnGoing:
                            win.ICOnGoing.Items.Add(ctrl);
                            break;
                        case TodoState.Done:
                            win.ICDone.Items.Add(ctrl);
                            break;
                    }
                }
            }
            else
            {
                this._Manager.Close();
                this._Manager = null;
            }

        }

        private void OnDrag(object sender, MouseButtonEventArgs e)
        {
            if (this._Manager != null)
            {
                this._Manager.Close();
                this._Manager = null;
            }
            this.DragMove();
        }

        private void OnClose(object sender, MouseButtonEventArgs e)
        {
            if (this._Manager != null)
            {
                this._Manager.Close();
                this._Manager = null;
            }
            this.Close();
        }

        private void OnClearTodos(object sender, MouseButtonEventArgs e)
        {
            ClearProjectsWindow win = new ClearProjectsWindow(this._Application, this._Manager)
            {
                Top = this.Top,
                Left = this.Left - 75,
                Topmost = true,
            };
            win.ShowDialog();
        }
    }
}
