using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Data.IDAO;
using Tracker.Data;

namespace Tracker.Data.DAO
{
    public class TrackerDAO :ITrackerDAO
    {
        private EventTrackerEntities _context;
        public TrackerDAO()
        {
            _context = new EventTrackerEntities(); 
        }

        //EVENT RELATED FUNCTIONS
        //Get a list of all the events within the database.
        public IList<tbl_events> GetEvents()
        {
            IQueryable<tbl_events> _events;
            _events = from tbl_events in _context.tbl_events select tbl_events;
            return _events.OrderBy(x => x.Event_Date).ToList<tbl_events>() ;
        }
        //Returns the details of a specific event.
        public tbl_events GetEventDetails(int Event_ID)
        {
            IQueryable<tbl_events> _events;
            _events = from tbl_events in _context.tbl_events where tbl_events.Event_ID == Event_ID select tbl_events ;
            return _events.First<tbl_events>();
        }

        //Create an Event
        public void CreateEvent(tbl_events _event)
        {
            _context.tbl_events.Add(_event);
            _context.SaveChanges();
        }

        //Edits the details of a specific event.
        public void EditEvent(tbl_events _events)
        {
            tbl_events _event = GetEventDetails(_events.Event_ID);

            _event.Event_ID = _events.Event_ID;
            _event.Event_Name = _events.Event_Name;
            _event.Event_Location = _events.Event_Location;
            _event.Event_Date = _events.Event_Date;
            _event.Upcoming = _events.Upcoming;
            _context.SaveChanges();
        }

        //Returns a list of an events lineup.
        public IList<tbl_eventlineup> GetLineUp(int Event_ID)
        {
            IQueryable<tbl_eventlineup> _lineup;
            _lineup = from tbl_eventlineup in _context.tbl_eventlineup where tbl_eventlineup.Event_ID == Event_ID select tbl_eventlineup;
            return _lineup.ToList<tbl_eventlineup>();
        }

        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        //Used to select the Event History of a specific user. Selects the Event_ID from tbl_usereventhistory, which is then to be used to display the records from tbl_events.
        public IList<tbl_eventhistory> GetUserEvents(string Id)
        {

            IQueryable<tbl_eventhistory> _eventHistory;
            _eventHistory = from tbl_eventhistory in _context.tbl_eventhistory where tbl_eventhistory.User_ID == Id select tbl_eventhistory;
            return _eventHistory.ToList<tbl_eventhistory>();

            
        }

        //-------------------------------------------------------------------------------
        // ARTIST RELATED FUNCTIONS
        //Gets a list of all of the artists within database.
        public IList<tbl_artists> GetArtists()
        {
            IQueryable <tbl_artists> _artists;
            _artists = from tbl_artists in _context.tbl_artists select tbl_artists;
            return _artists.ToList<tbl_artists>();
        }

        public tbl_artists GetArtistDetails(int Artist_ID)
        {
            IQueryable<tbl_artists> _artists;
            _artists = from tbl_artists in _context.tbl_artists where tbl_artists.Artist_ID == Artist_ID select tbl_artists;
            return _artists.First<tbl_artists>();
        }

        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.
        public IList<tbl_users> GetUsers()
        {
            IQueryable<tbl_users> _users;
            _users = from tbl_users in _context.tbl_users select tbl_users;
            return _users.ToList<tbl_users>();
        }

        
    }
}
