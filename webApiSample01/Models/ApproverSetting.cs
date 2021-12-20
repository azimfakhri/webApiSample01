using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace webApiSample01.Models
{
    public partial class ApproverSetting
    {
        public ApproverSetting()
        {
            ApproverList = new HashSet<ApproverList>();
        }

        public int SettingId { get; set; }
        public string SettingDescription { get; set; }
        public int UserRole { get; set; }
        public int NumberApprover { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual UserRoles UserRoleNavigation { get; set; }
        public virtual ICollection<ApproverList> ApproverList { get; set; }
    }
}
