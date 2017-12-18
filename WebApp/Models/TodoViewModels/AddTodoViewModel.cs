using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.TodoViewModels
{
    public class AddTodoViewModel
    {

        [Required]
        [Display(Name = "Description")]
        public string Text { get; set; }

        [Display(Name = "Date due")]
        [ValidateDateRange]
        [DataType(DataType.Date)]
        public Nullable<DateTime> DateDue { get; set; }

        public string Labels { get; set; }
    }

    public class ValidateDateRange : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            DateTime val = (DateTime)value;
            // your validation logic
            if (val >= Convert.ToDateTime("1753/1/2") && val <= Convert.ToDateTime("9999/12/31"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not in given range of  1753/1/2 and 9999/12/31");
            }
        }
    }
}
