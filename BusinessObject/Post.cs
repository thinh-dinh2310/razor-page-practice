using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace BusinessObject
{
    public partial class Post
    {
        public Post()
        {
            ApplicantPosts = new HashSet<ApplicantPost>();
            LocationPosts = new HashSet<LocationPost>();
            PostSkills = new HashSet<PostSkill>();
        }

        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Title must not be empty")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Invalid length(3->100) ")]
        public string Title { get; set; }

        public string Description { get; set; }
        public Guid LocationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid LevelId { get; set; }
        public int Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<ApplicantPost> ApplicantPosts { get; set; }
        public virtual ICollection<LocationPost> LocationPosts { get; set; }
        public virtual ICollection<PostSkill> PostSkills { get; set; }
    }
}
