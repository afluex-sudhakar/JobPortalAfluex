//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserNotification
    {
        public int Id { get; set; }
        public Nullable<int> NotificationId { get; set; }
        public Nullable<int> UserDeviceId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Medium { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual User User { get; set; }
        public virtual Notification Notification { get; set; }
    }
}