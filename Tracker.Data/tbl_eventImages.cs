
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

    public partial class tbl_eventImages
{
        [Key]
        public int File_ID { get; set; }

    public string File_Name { get; set; }

    public string Content_Type { get; set; }

    public byte[] Content { get; set; }

    public int Event_ID { get; set; }



    public virtual tbl_events tbl_events { get; set; }

}

}
