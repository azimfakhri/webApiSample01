using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace webApiSample01.Models
{
    public partial class UserRoles
    {
        public UserRoles()
        {
            ApproverSetting = new HashSet<ApproverSetting>();
            LeaveType = new HashSet<LeaveType>();
            UserAccount = new HashSet<UserAccount>();
        }

        public int RoleId { get; set; }
        public string UserRole { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual ICollection<ApproverSetting> ApproverSetting { get; set; }
        public virtual ICollection<LeaveType> LeaveType { get; set; }
        public virtual ICollection<UserAccount> UserAccount { get; set; }
    }
}
