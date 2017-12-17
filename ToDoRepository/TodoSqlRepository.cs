using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoRepository
{
    public class TodoSqlRepository : ITodoRepository
    {

        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }
        public void Add(TodoItem todoItem)
        {
            if(_context.TodoItems.Contains(todoItem))
            {
                throw new DuplicateTodoItemException(todoItem.Id);
            }

            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.First(t => t.Id.Equals(todoId));
            if (item == null)
            {
                return null;
            }
            else if(!item.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(userId, todoId);
            }

            return item;
        }

        public List<TodoItem> GetActive(Guid userId) => _context.TodoItems.Where(t => t.UserId.Equals(userId) && t.DateCompleted != null).ToList();

        public List<TodoItem> GetAll(Guid userId) => _context.TodoItems
                .Where(t => t.UserId.Equals(userId))
                .OrderBy(t => t.DateCreated)
                .ToList();

        public List<TodoItem> GetCompleted(Guid userId) => _context.TodoItems.Where(t => t.UserId.Equals(userId) && t.isCompleted).ToList();

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId) => _context.TodoItems.Where(t => filterFunction(t)).ToList();

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);
            bool result = item.MarkAsCompleted();
            _context.SaveChanges();

            return result;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem item = Get(todoId, userId);

            if(item == null)
            {
                return false;
            }

            _context.TodoItems.Remove(item);
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem item = _context.TodoItems.First(t => t.Equals(todoItem));
            if(item == null)
            {
                _context.TodoItems.Add(todoItem);
            }

            if(!item.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException(userId, todoItem.Id);
            }

            _context.TodoItems.Remove(item);
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }
    }

    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(Guid id) : base("duplicate id: " + id.ToString())
        {
        }
    }

    public class TodoAccessDeniedException : Exception
    {

        public TodoAccessDeniedException(Guid userId, Guid todoId) : base("User " + userId.ToString() + " can not access todoItem " + todoId.ToString())
        {
        }
    }
}
