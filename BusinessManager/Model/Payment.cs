//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessManager.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> StudentId { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        public virtual Student Student { get; set; }
    }
}