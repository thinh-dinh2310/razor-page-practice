using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Status
    {
        public Status()
        {
            ApplicantPosts = new HashSet<ApplicantPost>();
            Posts = new HashSet<Post>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<ApplicantPost> ApplicantPosts { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
