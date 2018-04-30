using System;
using System.IO;
using Newtonsoft.Json;

namespace TodoManager.Models
{
    internal enum TodoState
    {
        Todo = 1,
        OnGoing = 2,
        Done = 3,
    }

    internal class Todo
    {
        internal static ulong CurrentID = 0;

        private string _Name;
        private string _Description;
        private DateTime? _Deadline;
        private TodoState _State;
        private ulong _ID;

        internal Todo(string name,string desc,DateTime? deadline=null)
        {
            this._Name        = name;
            this._Description = desc;
            this._Deadline    = deadline;
            this._State       = TodoState.Todo;
            this._ID          = CurrentID;
            CurrentID++;
        }

        public Todo()
        {
            //This is for serializing
        }

        public string       Name        { get => this._Name;     set => this._Name        = value; }
        public string       Description { get => this._Name;     set => this._Description = value; }
        public DateTime?    Deadline    { get => this._Deadline; set => this._Deadline    = value; }
        public TodoState    State       { get => this._State;    set => this._State       = value; }
        public ulong        ID          { get => this._ID;       set => this._ID          = value; }

        internal bool TrySerialize(out string json)
        {
            try
            {
                json = JsonConvert.SerializeObject(this);
                return true;
            }
            catch
            {
                json = string.Empty;
                return false;
            }
        }

        internal static bool TryLoad(string path,out Todo todo)
        {
            try
            {
                string json = File.ReadAllText(path);
                todo = JsonConvert.DeserializeObject<Todo>(json);
                return true;
            }
            catch
            {
                todo = null;
                return false;
            }
        }
    }
}
