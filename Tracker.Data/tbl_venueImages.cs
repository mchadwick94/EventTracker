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
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_venueImages
    {
        [Key]
        public int V_FileID { get; set; }
        public string V_FileName { get; set; }
        public string V_ContentType { get; set; }
        public byte[] V_Content { get; set; }
        public int Venue_ID { get; set; }
    
        public virtual tbl_venues tbl_venues { get; set; }
    }
}
