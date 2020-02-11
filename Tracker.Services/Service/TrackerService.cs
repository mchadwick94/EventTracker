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

        //Allows users to add artists to a specific events lineup
        public void addToLineup(tbl_eventlineup _lineup)
        {
            _TrackerDAO.addToLineup(_lineup);
        }

        //Retrieves the details of a lineup entry, this is used for automatic removal of an artist from a lineup using the 'deleteFromLineup' function below.
        public tbl_eventlineup GetLineupDetails(int Lineup_ID)
        {
            return _TrackerDAO.GetLineupDetails(Lineup_ID);
        }

        //Allows users to remove an artist from a specific events lineup
        public void deleteFromLineup(tbl_eventlineup _lineup)
        {
            _TrackerDAO.deleteFromLineup(_lineup);
        }

        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        //Displays the full list of events for a specific user.
        public IList<tbl_eventhistory> GetUserEvents(string User_ID)
        {
            return _TrackerDAO.GetUserEvents(User_ID);
        }

        //Allows users to add an event to their event history.
        public void AddToUser(tbl_eventhistory _event)
        {
            _TrackerDAO.AddToUser(_event);
        }

        //Retrieves a specific event in a users history, this is used for the below function of removing an event from a users history.
        public tbl_eventhistory GetEventHistoryDetails(int Event_ID)
        {
            return _TrackerDAO.GetEventHistoryDetails(Event_ID);
        }

        //Allows users to remove an event from their history.
        public void deleteFromUserHistory(tbl_eventhistory _event)
        {
            _TrackerDAO.deleteFromUserHistory(_event);
        }

        //WORKING PROGRESS
        public IList<tbl_artisthistory> GetHistoryLineup(int EventLineup_ID)
        {
            return _TrackerDAO.GetHistoryLineup(EventLineup_ID);
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

        //Allows users to add a new artist to the database
        public void NewArtist(tbl_artists _artist)
        {
            _TrackerDAO.NewArtist(_artist);
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
