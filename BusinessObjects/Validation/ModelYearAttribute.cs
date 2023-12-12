using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Validation
{
    public class ModelYearAttribute : ValidationAttribute
    {
        public ModelYearAttribute()
        {
            ErrorMessage = "The year cannot greeter than current year (" + DateTime.Now.Year.ToString() + ")";
        }

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }
            int year = Convert.ToInt32(value);
            return (year <= DateTime.Now.Year);
        }
    }
}
