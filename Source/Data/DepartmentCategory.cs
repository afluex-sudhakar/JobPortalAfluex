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
    
    public partial class DepartmentCategory
    {
        public int Id { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Category Category { get; set; }
    }
}
