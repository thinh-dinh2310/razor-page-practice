using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class RequiredSkills : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var post = (PostForCreationDto)validationContext.ObjectInstance;

            if (post.PostSkillsIds == null)
                return new ValidationResult("Skills is required");
            else
                return ValidationResult.Success;
        }
    }
}
