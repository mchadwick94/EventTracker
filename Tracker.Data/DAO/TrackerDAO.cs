using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Data.IDAO;

namespace Tracker.Data.DAO
{
    public class TrackerDAO :ITrackerDAO
    {
        private TrackerEntities _context;
        public TrackerDAO()
        {
            _context = new TrackerEntities();
        }

        public IList<tbl_events> GetEvents()
        {
            IQueryable<tbl_events> _events;
            _events = from tbl_events in _context.tbl_events select tbl_events;
            return _events.ToList<tbl_events>();
        }

        //Used to select the Event History of a specific user. Selects the Event_ID from tbl_usereventhistory, which is then to be used to display the records from tbl_events.
        public IList<tbl_eventhistory> GetUserEvents(int User_ID)
        {
            IQueryable<tbl_eventhistory> _eventHistory;
            _eventHistory = from tbl_eventhistory in _context.tbl_eventhistory where tbl_eventhistory.User_ID == User_ID select tbl_eventhistory;
            return _eventHistory.ToList<tbl_eventhistory>();
            
        }





        public IList<tbl_artists> GetArtists()
        {
            IQueryable <tbl_artists> _artists;
            _artists = from tbl_artists in _context.tbl_artists select tbl_artists;
            return _artists.ToList<tbl_artists>();
        }
        public IList<tbl_users> GetUsers()
        {
            IQueryable<tbl_users> _users;
            _users = from tbl_users in _context.tbl_users select tbl_users;
            return _users.ToList<tbl_users>();
        }

        
    }
}
