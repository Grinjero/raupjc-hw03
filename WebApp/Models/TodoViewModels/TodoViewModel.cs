using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace WebApp.Models.TodoViewModels
{
    public class TodoViewModel
    {
        [Required]
        [MaxLength(400)]
        [Display(Name = "Description")]
        public string Text { get; set; }

        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> DateDue { get; set; }

        public Nullable<int> DaysLeft { get; set; }

        public Nullable<DateTime> DateCompleted { get; set; }

        public string DateDueText { get; set; }

        public Guid userId { get; set; }
    }
}
