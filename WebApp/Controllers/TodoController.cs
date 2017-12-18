using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models.TodoViewModels;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ITodoRepository _repository;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public Guid getUserId()
        {
            return new Guid(_userManager.GetUserId(HttpContext.User));
        }

        public ActionResult Index()
        {
            Guid userId = getUserId();

            List<TodoViewModel> todoViewList = new List<TodoViewModel>();
            List<TodoItem> todoItems = _repository.GetActive(userId);
            foreach (TodoItem item in todoItems)
            {
                TodoViewModel todoView = new TodoViewModel();
                todoView.Text = item.Text;
                todoView.DateDue = item.DateDue;

                if (item.DateDue.HasValue)
                {
                    todoView.DateDueText = generateDateText(todoView.DateDue.Value);
                    todoView.DaysLeft = (item.DateDue - DateTime.Now).Value.Days;
                }

                todoView.Id = item.Id;

                todoViewList.Add(todoView);
            }

            return View(todoViewList);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddTodoViewModel todoViewModel)
        {
            Guid userId = getUserId();

            if (!ModelState.IsValid)
            {
                return View(todoViewModel);
            }

            TodoItem item = new TodoItem();

            if(todoViewModel.Labels != null)
            {
                string[] splits = todoViewModel.Labels.Split(',');
                List<TodoLabel> labelList = new List<TodoLabel>();
                
                foreach(string label in splits)
                {
                    TodoLabel todoLabel = new TodoLabel(label.Trim());

                    labelList.Add(todoLabel);

                }

                item.Labels = labelList;
            }

            if (todoViewModel.DateDue.Equals(DateTime.MinValue))
            {
                item.DateDue = new Nullable<DateTime>();
            }
            else
            {
                item.DateDue = todoViewModel.DateDue;
            }
            item.Text = todoViewModel.Text;
            item.UserId = userId;
            item.DateCompleted = new Nullable<DateTime>();

            

            _repository.Add(item);

            return RedirectToAction("Index");
        }

        public ActionResult Completed()
        {
            Guid userId = getUserId();

            List<TodoViewModel> todoModels = new List<TodoViewModel>();

            foreach(TodoItem completed in _repository.GetCompleted(userId))
            {
                TodoViewModel todoModel = new TodoViewModel();
                todoModel.DateCompleted = completed.DateCompleted;
                todoModel.Text = completed.Text;
                todoModel.DateDueText = generateDateText(completed.DateCompleted.Value);
                todoModel.Id = completed.Id;
                todoModels.Add(todoModel);
                
            }

            return View(todoModels);
        }

        public ActionResult MarkCompleted(Guid id)
        {
            Guid userId = getUserId();

            _repository.MarkAsCompleted(id, userId);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(Guid id)
        {
            Guid userId = getUserId();

            _repository.Remove(id, userId);
            return RedirectToAction("Completed");
        }

        public string generateDateText(DateTime date)
        {
            string text = date.Day + ". ";

            switch(date.Month)
            {
                case (1):
                    text += "siječnja";
                    break;
                case (2):
                    text += "veljače";
                    break;
                case (3):
                    text += "ožujka";
                    break;
                case (4):
                    text += "travnja";
                    break;
                case (5):
                    text += "svibnja";
                    break;
                case (6):
                    text += "lipnja";
                    break;
                case (7):
                    text += "srpnja";
                    break;
                case (8):
                    text += "kolovoza";
                    break;
                case (9):
                    text += "rujna";
                    break;
                case (10):
                    text += "listopada";
                    break;
                case (11):
                    text += "studenog";
                    break;
                case (12):
                    text += "prosinca";
                    break;
            }

            text += " " + date.Year + ".";
            return text;
        }
    }
}
