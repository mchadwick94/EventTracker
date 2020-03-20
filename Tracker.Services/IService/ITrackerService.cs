using System.Collections.Generic;
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

        //Allows artists to be added to the lineup
        void AddToLineup(tbl_eventlineup _lineup);

        //Returns the details of a specific lineup entry
        Tracker.Data.tbl_eventlineup GetLineupDetails(int Lineup_ID);

        //Removes an artist from a specific events lineup
        void DeleteFromLineup(tbl_eventlineup _lineup);

        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        // Returns a list of all the events for a specific user.
        IList<Tracker.Data.tbl_eventhistory> GetUserEvents(string User_ID);

        //Allows a user to add an event to their own event history
        void AddToUser(tbl_eventhistory _event);

        //Retrieves a list of artists on the lineup of an event within a users event history
        IList<Tracker.Data.tbl_artisthistory> GetHistoryLineup(int EventLineup_ID);

        //Retrieves the details of an event within the users event history
        Tracker.Data.tbl_eventhistory GetEventHistoryDetails(int Event_ID);

        //Retrieves the details of an event within the users event history
        void DeleteFromUserHistory(tbl_eventhistory _event);

        //Returns a list of an events lineup through the user events.
        IList<Tracker.Data.tbl_eventlineup> GetUsersLineUp(int Event_ID);

        void AddToArtistHistory(tbl_artisthistory _entry);

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
