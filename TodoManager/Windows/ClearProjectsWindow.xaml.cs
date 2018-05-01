using System;
using System.Collections.Generic;
using System.IO;
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

namespace TodoManager.Windows
{
    /// <summary>
    /// Interaction logic for ClearProjectsWindow.xaml
    /// </summary>
    public partial class ClearProjectsWindow : Window
    {
        private ManagerWindow _Manager;
        private App _Application;

        internal ClearProjectsWindow(App app,ManagerWindow win)
        {
            this._Application = app;
            this._Manager = win;
            this.InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            string[] files = Directory.GetFiles("Todos");
            foreach (string path in files)
                File.Delete(path);
            this._Application.Todos.Clear();

            if (this._Manager != null)
            {
                this._Manager.ICTodo.Items.Clear();
                this._Manager.ICOnGoing.Items.Clear();
                this._Manager.ICDone.Items.Clear();
            }
        }
    }
}
