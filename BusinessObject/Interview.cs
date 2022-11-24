using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject
{
    public partial class Interview
    {
        public Guid InterviewerId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Feedback { get; set; }
        public bool Result { get; set; }
        public int Round { get; set; }
        public Guid PostId { get; set; }
        public Guid ApplicantId { get; set; }

        public virtual ApplicantPost ApplicantPost { get; set; }
        public virtual User Interviewer { get; set; }
    }
}
