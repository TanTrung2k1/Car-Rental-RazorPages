using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Validation
{
    public class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute()
        {
            ErrorMessage = "Enter email correct format (abc@FURentalSystem.com)";
        }
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }
            string email = value as string;
            return (email.Contains("@FURentalSystem.com"));
        }
    }
}
