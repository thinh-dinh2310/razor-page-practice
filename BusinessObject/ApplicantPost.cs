using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable enable

namespace BusinessObject
{

    public partial class ApplicantPost
    {

        public ApplicantPost()
        {
            Interviews = new HashSet<Interview>();
        }

        public Guid ApplicantId { get; set; }
        public Guid PostId { get; set; }
        public byte[]? Resume { get; set; }
        [Required(ErrorMessage = "Message can't be empty")]
        public string? Message { get; set; }
        public int? Count { get; set; }
        public int? Status { get; set; }

        public virtual User Applicant { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
