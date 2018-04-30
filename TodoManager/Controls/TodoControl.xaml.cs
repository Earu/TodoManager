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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            }
            this._Manager.SelectedControl = this;
        }
    }
}
