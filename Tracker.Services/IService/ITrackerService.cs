using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Services.IService
{
    public interface ITrackerService
    {
        //EVENT RELATED FUNCTIONS
        //Get a list of all the events within the database.
        IList<Tracker.Data.tbl_events> GetEvents();

        //Returns the details of a specific event.
        Tracker.Data.tbl_events GetEventDetails(int Event_ID);

        //Returns a list of an events lineup.
        IList<Tracker.Data.tbl_eventlineup> GetLineUp(int Event_ID);
        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        // Returns a list of all the events for a specific user.
        IList<Tracker.Data.tbl_eventhistory> GetUserEvents(int Event_ID);

        //-------------------------------------------------------------------------------
        // ARTIST RELATED FUNCTIONS
        //Gets a list of all of the artists within database.
        IList<Tracker.Data.tbl_artists> GetArtists();

        //Returns the details of a specific artist.
        Tracker.Data.tbl_artists GetArtistDetails(int Artist_ID);
        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.
        IList<Tracker.Data.tbl_users> GetUsers();
    }
}
