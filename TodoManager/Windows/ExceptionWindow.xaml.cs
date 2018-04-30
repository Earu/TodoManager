using System.Windows;

namespace TodoManager.Windows
{
    /// <summary>
    /// Interaction logic for ExceptionWindow.xaml
    /// </summary>
    public partial class ExceptionWindow : Window
    {
        internal ExceptionWindow()
        {
            this.InitializeComponent();
        }

        internal void SetText(string text)
        {
            this.Exception.Text = text;
        }

        private void OnOK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
