using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class SkillNameUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            string tag = value as string;
            if (string.IsNullOrWhiteSpace(tag))
                return new ValidationResult("Name is required.");

            using (var dbContext = new eRecruitment_PRN221Context())
            {
                if (dbContext.Skills.Any(td => td.SkillName.ToLower().Equals(tag.ToLower())))
                {
                    return new ValidationResult("This Skill Name can't be duplicate");
                }
            }

            return ValidationResult.Success;
        }
    }
}
