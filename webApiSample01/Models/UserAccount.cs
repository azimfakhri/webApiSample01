using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace webApiSample01.Models
{
    public partial class UserAccount
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserFullName { get; set; }
        public string UserPassword { get; set; }
        public int UserRole { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual UserRoles UserRoleNavigation { get; set; }
    }
}
