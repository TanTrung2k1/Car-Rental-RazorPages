using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Validation
{
    public class TodayDateAttribute : ValidationAttribute
    {
        public TodayDateAttribute()
        {
            ErrorMessage = "The date entered cannot be a past date.";
        }
        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                if (date.Date < DateTime.Today)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
