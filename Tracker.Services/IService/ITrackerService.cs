using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Data;

namespace Tracker.Services.IService
{
    public interface ITrackerService
    {
        //EVENT RELATED FUNCTIONS
        //Get a list of all the events within the database.
        IList<Tracker.Data.tbl_events> GetEvents();

        //Returns the details of a specific event.
        Tracker.Data.tbl_events GetEventDetails(int Event_ID);

        //Create a new Event
        void CreateEvent(tbl_events _event);


        //Edits the details of a specific event.
        void EditEvent(tbl_events _events);


        //Returns a list of an events lineup.
        IList<Tracker.Data.tbl_eventlineup> GetLineUp(int Event_ID);



        Tracker.Data.tbl_eventlineup GetLineupDetails(int Lineup_ID);

        void addToLineup(tbl_eventlineup _lineup);

        void deleteFromLineup(tbl_eventlineup _lineup);

        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        // Returns a list of all the events for a specific user.
        IList<Tracker.Data.tbl_eventhistory> GetUserEvents(string User_ID);

        void AddToUser(tbl_eventhistory _event);

        Tracker.Data.tbl_eventhistory GetEventHistoryDetails(int Event_ID);

        void deleteFromUserHistory(tbl_eventhistory _event);

        IList<Tracker.Data.tbl_artisthistory> GetHistoryLineup(int EventLineup_ID);

        //Returns a list of an events lineup through the user events.
        IList<Tracker.Data.tbl_eventlineup> GetUsersLineUp(int Event_ID);

        //-------------------------------------------------------------------------------
        // ARTIST RELATED FUNCTIONS
        //Gets a list of all of the artists within database.
        IList<Tracker.Data.tbl_artists> GetArtists();

        //Returns the details of a specific artist.
        Tracker.Data.tbl_artists GetArtistDetails(int Artist_ID);

        void NewArtist(tbl_artists _artist);
        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.
        IList<Tracker.Data.tbl_users> GetUsers();
    }
}
