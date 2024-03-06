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
    
    public partial class Notification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Notification()
        {
            this.UserNotifications = new HashSet<UserNotification>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleH { get; set; }
        public string Description { get; set; }
        public string DescriptionH { get; set; }
        public string Link { get; set; }
        public string Logo { get; set; }
        public bool IsSchedule { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public string Image { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
