using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.TodoViewModels
{
    public class TodoViewModel
    {
        [Required]
        [MaxLength(400)]
        [Display(Name = "Description")]
        public string Text { get; set; }

        public Nullable<DateTime> DateDue { get; set; }

        public Nullable<int> DaysLeft { get; set; }
    }
}
