using System.Collections.Generic;
using System.IO;
using System.Windows;
using TodoManager.Models;
using TodoManager.Windows;

namespace TodoManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<Todo> _Todos;

        internal App()
        {
            this.InitializeTodos();
            MainWindow win = new MainWindow(this);
            win.Show();
        }

        internal List<Todo> Todos { get => this._Todos; }

        private void InitializeTodos()
        {
            this._Todos = new List<Todo>();
            if (!Directory.Exists("Todos"))
            {
                Directory.CreateDirectory("Todos");
                return;
            }

            string[] files = Directory.GetFiles("Todos");
            foreach(string path in files)
                if(Todo.TryLoad(path, out Todo proj))
                    this._Todos.Add(proj);

            if (this._Todos.Count == 0) return;
            ulong lastid = this.GetHighestID();
            Todo.CurrentID = lastid + 1;
        }

        private ulong GetHighestID()
        {
            ulong max = 0;
            foreach(Todo t in this._Todos)
                if (max < t.ID)
                    max = t.ID;

            return max;
        }

        internal bool TrySaveTodos()
        {
            foreach(Todo proj in this._Todos)
            {
                if(proj.TrySerialize(out string json))
                {
                    try
                    {
                        File.WriteAllText($"Todos/{proj.ID}.proj", json);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
