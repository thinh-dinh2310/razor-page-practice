using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BusinessObject
{
    public partial class Location
    {
        public Location()
        {
            LocationPosts = new HashSet<LocationPost>();
        }

        public Guid LocationId { get; set; }
        [Required]
        [LocationNameUnique]
        public string LocationName { get; set; }

        public virtual ICollection<LocationPost> LocationPosts { get; set; }
    }
}
