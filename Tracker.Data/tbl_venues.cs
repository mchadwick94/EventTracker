
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
    
public partial class tbl_venues
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public tbl_venues()
    {

        this.tbl_events = new HashSet<tbl_events>();

    }


    public int Venue_ID { get; set; }

    public string V_Name { get; set; }

    public string V_StreetAddress { get; set; }

    public string V_City { get; set; }

    public int V_Country { get; set; }

    public string V_Postcode { get; set; }



    public virtual tbl_countries tbl_countries { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<tbl_events> tbl_events { get; set; }

}

}
