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

        void AddEventImage(tbl_eventImages image);

        void EditEventImage(tbl_eventImages oldImage, tbl_eventImages newImage);

        void RemoveEventImage(tbl_eventImages image);

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

        void AddToArtistHistory(tbl_artisthistory _entry);

        IList<Tracker.Data.tbl_artisthistory> GetSeenArtists(string User_ID);

        Tracker.Data.tbl_artisthistory GetSeenArtistDetails(int ArtistHistory_ID);

        Tracker.Data.tbl_artisthistory FindSeenArtistEntry(int Lineup_ID, int Event_ID, int Artist_ID, string User_ID);

        void DeleteFromSeenArtists(tbl_artisthistory _entry);

        //-------------------------------------------------------------------------------
        // ARTIST RELATED FUNCTIONS
        //Gets a list of all of the artists within database.
        IList<Tracker.Data.tbl_artists> GetArtists();

        //Returns the details of a specific artist.
        Tracker.Data.tbl_artists GetArtistDetails(int Artist_ID);

        void NewArtist(tbl_artists _artist);

        void EditArtist(tbl_artists _artists);

        void AddArtistImage(tbl_artistImages image);

        void EditArtistImage(tbl_artistImages oldImage, tbl_artistImages newImage);

        void RemoveArtistImage(tbl_artistImages image);

        IList<tbl_eventlineup> GetAnArtistsEventHistory(int Artist_ID);

        //-------------------------------------------------------------------------------
        // USER/ARTIST RELATED FUNCTIONS
        IList<Tracker.Data.tbl_artisthistory> GetSeenArtistHistory(string User_ID, int Artist_ID);

        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.

        //-------------------------------------------------------------------------------
        // VENUE RELATED FUNCTIONS
        IList<Tracker.Data.tbl_venues> GetVenues();

        IList<tbl_venues> GetVenuesByCity(int City_ID);

        IList<tbl_venues> GetVenuesByCountry(string C_Iso);

        Tracker.Data.tbl_venues GetVenueDetails(int Venue_ID);

        void CreateVenue(tbl_venues _venue);

        void EditVenue(tbl_venues _venues);

        void DeleteVenue(tbl_venues venue);

        void AddVenueImage(tbl_venueImages image);

        void EditVenueImage(tbl_venueImages oldImage, tbl_venueImages newImage);

        void RemoveVenueImage(tbl_venueImages image);

        //-----------------------------------------------------------------------------------------------------------------------
        // Country RELATED FUNCTIONS
        //Gets a list of all the countries within the database.
        IList<Tracker.Data.tbl_countries> GetCountries();

        IList<tbl_cities> GetCities(string Country_ISO);

        //-------------------------------------------------------------------------------
        // File RELATED FUNCTIONS

        tbl_artistImages GetImageId(int Artist_ID);

        tbl_eventImages GetEventImageId(int Event_ID);

        tbl_venueImages GetVenueImageId(int Venue_ID);
        }
    }