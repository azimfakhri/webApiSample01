using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace webApiSample01.Models
{
    public partial class LeaveSubmission
    {
        public int LeaveSubmissionId { get; set; }
        public int StaffId { get; set; }
        public int LeaveType { get; set; }
        public int LeaveDuration { get; set; }
        public int LeaveStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual LeaveType LeaveSubmission1 { get; set; }
        public virtual LeaveStatus LeaveSubmissionNavigation { get; set; }
    }
}
