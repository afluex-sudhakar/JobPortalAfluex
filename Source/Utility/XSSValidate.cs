using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Utility
{
    public class XSSValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object text, ValidationContext validationContext)
        {
            if (text != null && text.ToString() != "")
            {
                var regx = new Regex(@"<[^>]+>|&lt\;[^>]+&gt\;|lt\;[^>]+gt\;", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                if (!regx.IsMatch(text.ToString()))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Characters not allowed");
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}