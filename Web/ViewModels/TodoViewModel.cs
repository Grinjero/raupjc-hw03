using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class TodoViewModel
    {
        public string Text { get; set; }

        public Nullable<DateTime> DateDue { get; set; }

        public Nullable<int> DaysLeft { get; set; }
    }
}
