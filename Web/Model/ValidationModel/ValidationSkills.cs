using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model.ValidationModel
{
    public class ValidationSkills : ValidationAttribute
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
