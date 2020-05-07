using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tracker.Data;

namespace EventTracker.Models
{
    public class SeenArtistQuantities
    {
        public int ArtistHistory_ID { get; set; }
        public string User_ID { get; set; }
        public int Event_ID { get; set; }
        public int EventLineup_ID { get; set; }
        public int Artist_ID { get; set; }
        public string Artist_Name { get; set; }
        public int Count { get; set; }

        public virtual tbl_artists tbl_artists { get; set; }
        public virtual tbl_events tbl_events { get; set; }
        public virtual tbl_eventlineup tbl_eventlineup { get; set; }
    }

    public class SeenArtistCount
    {
        [Key]
        [Column(Order = 0)]
        public int Artist_ID { get; set; }

        public string Artist_Name { get; set; }
        public int c { get; set; }
        public virtual tbl_artists tbl_artists { get; set; }
        public virtual tbl_eventlineup tbl_artisthistory { get; set; }

        public virtual tbl_artistImages tbl_artistImages { get; set; }
    }
}