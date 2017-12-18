using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoRepository

{
    public class TodoItem
    {

        public Guid Id { get; set; }
        public string Text { get; set; }

        public Guid UserId { get; set; }

        public List<TodoLabel> Labels { get; set; }

        public Nullable<DateTime> DateDue { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.Now;
            UserId = userId;
            Labels = new List<TodoLabel>();
        }

        public TodoItem() {
            DateCreated = DateTime.Now;
            Id = Guid.NewGuid();
        }

        public bool isCompleted
        {
            get
            {
                return DateCompleted.HasValue;
            }
        }
        
        public Nullable<DateTime> DateCompleted { get; set; }

        public DateTime DateCreated { get; set; }

        public bool MarkAsCompleted()
        {
            if (!isCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid();

            DateCreated = DateTime.UtcNow;

            Text = text;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            TodoItem other = (TodoItem)obj;

            return other.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TodoLabel
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public List<TodoItem> LabelTodoItems { get; set; }

        public TodoLabel(string value)
        {
            Id = Guid.NewGuid();
            Value = value;
            LabelTodoItems = new List<TodoItem>();
        }

        public TodoLabel()
        {
            Id = Guid.NewGuid();
            LabelTodoItems = new List<TodoItem>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            TodoLabel other = (TodoLabel)obj;

            return other.Value.Equals(this.Value);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
