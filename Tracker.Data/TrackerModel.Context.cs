﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TrackerEntities : DbContext
    {
        public TrackerEntities()
            : base("name=TrackerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_artisthistory> tbl_artisthistory { get; set; }
        public virtual DbSet<tbl_artistImages> tbl_artistImages { get; set; }
        public virtual DbSet<tbl_artists> tbl_artists { get; set; }
        public virtual DbSet<tbl_cities> tbl_cities { get; set; }
        public virtual DbSet<tbl_countries> tbl_countries { get; set; }
        public virtual DbSet<tbl_eventhistory> tbl_eventhistory { get; set; }
        public virtual DbSet<tbl_eventImages> tbl_eventImages { get; set; }
        public virtual DbSet<tbl_eventlineup> tbl_eventlineup { get; set; }
        public virtual DbSet<tbl_events> tbl_events { get; set; }
        public virtual DbSet<tbl_venueImages> tbl_venueImages { get; set; }
        public virtual DbSet<tbl_venues> tbl_venues { get; set; }
    }
}
