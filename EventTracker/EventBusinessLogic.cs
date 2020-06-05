using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Data;
using EventTracker.Models;

namespace EventTracker
    {
    public class EventBusinessLogic
        {
        private TrackerEntities _context;

        public EventBusinessLogic()
            {
            _context = new TrackerEntities();
            }

        public IQueryable<tbl_events> GetFilteredEvents(SearchEventModel searchModel) //https://stackoverflow.com/questions/33153932/filter-search-using-multiple-fields-asp-net-mvc/33154580
            {
            var result = _context.tbl_events.AsQueryable();
            if (searchModel != null)
                {
                if (!string.IsNullOrEmpty(searchModel.Event_Country))
                    result = result.Where(x => x.tbl_venues.tbl_cities.CC_ISO == searchModel.Event_Country);
                if (searchModel.Event_City.HasValue)
                    result = result.Where(x => x.tbl_venues.V_City == searchModel.Event_City);
                if (searchModel.Event_Venue.HasValue)
                    result = result.Where(x => x.tbl_venues.Venue_ID == searchModel.Event_Venue);
                if (searchModel.Search_Start_Date.HasValue)
                    result = result.Where(x => x.Event_Date >= searchModel.Search_Start_Date);
                if (searchModel.Search_End_Date.HasValue)
                    result = result.Where(x => x.Event_Date <= searchModel.Search_End_Date);
                if (!string.IsNullOrEmpty(searchModel.Event_Name))
                    {
                    string EventName = searchModel.Event_Name;
                    string SearchString = EventName.Replace("**", " ");
                    Console.WriteLine(SearchString);
                    result = result.Where(x => x.Event_Name.Contains(SearchString));
                    }
                }
            return result;
            }
        }
    }