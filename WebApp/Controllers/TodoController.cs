using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.TodoViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {

        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index(Guid userId)
        {
            List<TodoViewModel> todoViewList = new List<TodoViewModel>();
            foreach(TodoItem item in _repository.GetActive(userId))
            {
                TodoViewModel todoView = new TodoViewModel();
                todoView.Text = item.Text;
                todoView.DateDue = item.DateDue;
                todoView.DaysLeft = (item.DateDue - DateTime.Now).Value.Days;

                todoViewList.Add(todoView);
            }

            return View(todoViewList);
        }

        public ActionResult Add(Guid userId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddTodoViewModel todoViewModel, Guid userId)
        {
            TodoItem item = new TodoItem();

            if(todoViewModel.Labels != null)
            {
                string[] splits = todoViewModel.Labels.Split(',');
                List<TodoLabel> labelList = new List<TodoLabel>();
                
                foreach(string label in splits)
                {
                    TodoLabel todoLabel = new TodoLabel(label);

                    labelList.Add(todoLabel);

                }

                item.Labels = labelList;
            }

            item.DateDue = todoViewModel.DateDue;
            item.Text = todoViewModel.Text;
            item.UserId = userId;

            _repository.Add(item);

            return RedirectToAction("Index");
        }
    }
}
