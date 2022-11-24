using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class User
    {
        public User()
        {
            ApplicantPosts = new HashSet<ApplicantPost>();
            Interviews = new HashSet<Interview>();
            UserSkills = new HashSet<UserSkill>();
        }

        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        [Required(ErrorMessage = "Email must not be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must not be empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Firstname must not be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname must not be empty")]
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public byte[]? Resume { get; set; }
        public byte[]? Avatar { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<ApplicantPost> ApplicantPosts { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
        public virtual ICollection<UserSkill> UserSkills { get; set; }
    }
}
