using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.TodoViewModels
{
    public class AddTodoViewModel
    {

        [Required]
        public string Text { get; set; }

        public DateTime DateDue { get; set; }

        public string Labels { get; set; }
    }
}
