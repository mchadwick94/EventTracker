//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tracker.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_events()
        {
            this.tbl_artisthistory = new HashSet<tbl_artisthistory>();
            this.tbl_eventhistory = new HashSet<tbl_eventhistory>();
            this.tbl_eventlineup = new HashSet<tbl_eventlineup>();
        }
    
        public int Event_ID { get; set; }
        public string Event_Name { get; set; }
        public System.DateTime Event_Date { get; set; }
        public string Event_Location { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_artisthistory> tbl_artisthistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_eventhistory> tbl_eventhistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_eventlineup> tbl_eventlineup { get; set; }
    }
}
