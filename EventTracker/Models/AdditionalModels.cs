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

    public class SearchEventModel
        {
        public int Event_ID { get; set; }

        public string Event_Name { get; set; }

        public System.DateTime? Search_Start_Date { get; set; }
        public System.DateTime? Search_End_Date { get; set; }

        public int? Event_City { get; set; }
        public int? Event_Venue { get; set; }
        public string Event_Country { get; set; }
        public virtual ICollection<tbl_eventImages> tbl_eventImages { get; set; }
        }

    public class CountryVM
        {
        public int Country { get; set; }
        public IEnumerable<CityVM> City_Names { get; set; }
        }

    public class CityVM
        {
        public int Value { get; set; }
        public string Text { get; set; }
        public IEnumerable<VenueVM> Venue_Names { get; set; }
        }

    public class VenueVM
        {
        public int V_Value { get; set; }
        public string V_Name { get; set; }
        }
    }