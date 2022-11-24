using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public Guid CategoryId { get; set; }
        [Required]
        [CategoryNameUnique]
        public string CategoryName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
