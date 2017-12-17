using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;

namespace Web
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
    }
}
