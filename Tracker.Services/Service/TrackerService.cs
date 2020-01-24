using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Data;
using Tracker.Data.IDAO;
using Tracker.Data.DAO;

namespace Tracker.Services.Service
{
    public class TrackerService : Tracker.Services.IService.ITrackerService
    {
        private ITrackerDAO _TrackerDAO;

        public TrackerService()
        {
            _TrackerDAO = new TrackerDAO();
        }
        //-------------------------------------------------------------------------------
        //EVENT RELATED FUNCTIONS
        //Get a list of all the events within the database.
        public IList<tbl_events> GetEvents()
        {
            return _TrackerDAO.GetEvents();
        }

        //Returns the details of a specific event.
        public tbl_events GetEventDetails(int Event_ID)
        {
            return _TrackerDAO.GetEventDetails(Event_ID);
        }

        //Create a new Event
        public void CreateEvent(tbl_events _event)
        {
            _TrackerDAO.CreateEvent(_event);
        }

        //Edits the details of a specific event.
        public void EditEvent(tbl_events _events)
        {
             _TrackerDAO.EditEvent(_events);
        }

        //Returns a list of an events lineup.
        public IList<tbl_eventlineup> GetLineUp(int Event_ID)
        {
            return _TrackerDAO.GetLineUp(Event_ID);
        }
        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        //Displays the full list of events for a specific user.
        public IList<tbl_eventhistory> GetUserEvents(int User_ID)
        {
            return _TrackerDAO.GetUserEvents(User_ID);
        }

        //-------------------------------------------------------------------------------
        // ARTIST RELATED FUNCTIONS
        //Gets a list of all of the artists within database.
        public IList<tbl_artists> GetArtists()
        {
            return _TrackerDAO.GetArtists();
        }

        //Returns the details of a specific artist.
        public tbl_artists GetArtistDetails(int Artist_ID)
        {
            return _TrackerDAO.GetArtistDetails(Artist_ID);
        }

        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.
        public IList<tbl_users> GetUsers()
        {
            return _TrackerDAO.GetUsers();
        }

        
    }
}
