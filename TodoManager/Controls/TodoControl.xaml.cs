using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TodoManager.Models;
using TodoManager.Windows;

namespace ProjectManager.Controls
{
    /// <summary>
    /// Interaction logic for TodoControl.xaml
    /// </summary>
    public partial class TodoControl : UserControl
    {
        private Todo _Todo;
        private ManagerWindow _Manager;

        internal TodoControl(Todo t,ManagerWindow win)
        {
            this._Todo = t;
            this._Manager = win;
            this.InitializeComponent();
            this.TBName.Text = t.Name;
        }

        internal Todo Todo { get => this._Todo; }

        private void OnSelected(object sender, MouseButtonEventArgs e)
        {
            BrushConverter cvter = new BrushConverter();
            this.RCBackground.Fill = (SolidColorBrush)(cvter.ConvertFrom("#444444"));
            if (this._Manager.SelectedControl != null)
            {
                this._Manager.SelectedControl.RCBackground.Fill = (SolidColorBrush)(cvter.ConvertFrom("#222222"));
                if (this._Manager.SelectedControl._Todo.ID == this._Todo.ID)
                {
                    AddTodoWindow win = new AddTodoWindow(this._Manager, this._Todo);
                    win.ShowDialog();
                }
            }
            this._Manager.SelectedControl = this;
        }
    }
}
