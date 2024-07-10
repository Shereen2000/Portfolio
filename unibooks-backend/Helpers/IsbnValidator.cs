using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;
using System.Threading.Tasks;

namespace unibooks_backend.Helpers
{
    public class IsbnValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                    string isbn = value.ToString();

                    string pattern =  @"^\d+$";

                    Regex regex = new Regex(pattern);

                    if(!regex.IsMatch(isbn))
                    {
                        return new ValidationResult("ISBN must only contain numeric characters.");
                    }
            }
                    
            return ValidationResult.Success;
        }
    }
}