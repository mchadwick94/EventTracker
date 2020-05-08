using System.Collections.Generic;
using Tracker.Data;
using Tracker.Data.DAO;
using Tracker.Data.IDAO;

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
        public void AddToLineup(tbl_eventlineup _lineup)
        {
            _TrackerDAO.AddToLineup(_lineup);
        }

        //Retrieves the details of a lineup entry, this is used for automatic removal of an artist from a lineup using the 'DeleteFromLineup' function below.
        public tbl_eventlineup GetLineupDetails(int Lineup_ID)
        {
            return _TrackerDAO.GetLineupDetails(Lineup_ID);
        }

        //Allows users to remove an artist from a specific events lineup
        public void DeleteFromLineup(tbl_eventlineup _lineup)
        {
            _TrackerDAO.DeleteFromLineup(_lineup);
        }

        public void AddEventImage(tbl_eventImages image)
        {
            _TrackerDAO.AddEventImage(image);
        }

        public void EditEventImage(tbl_eventImages oldImage, tbl_eventImages newImage)
        {
            _TrackerDAO.EditEventImage(oldImage, newImage);
        }

        public void RemoveEventImage(tbl_eventImages image)
        {
            _TrackerDAO.RemoveEventImage(image);
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

        //Retrieves a list of artists on the lineup of an event within a users event history
        public IList<tbl_artisthistory> GetHistoryLineup(int EventLineup_ID)
        {
            return _TrackerDAO.GetHistoryLineup(EventLineup_ID);
        }

        //Retrieves a specific event in a users history, this is used for the below function of removing an event from a users history.
        public tbl_eventhistory GetEventHistoryDetails(int Event_ID)
        {
            return _TrackerDAO.GetEventHistoryDetails(Event_ID);
        }

        //Allows users to remove an event from their history.
        public void DeleteFromUserHistory(tbl_eventhistory _event)
        {
            _TrackerDAO.DeleteFromUserHistory(_event);
        }

        //Returns a list of an events lineup through the users events.
        public IList<tbl_eventlineup> GetUsersLineUp(int Event_ID)
        {
            return _TrackerDAO.GetUsersLineUp(Event_ID);
        }

        public void AddToArtistHistory(tbl_artisthistory _entry)
        {
            _TrackerDAO.AddToArtistHistory(_entry);
        }

        public IList<tbl_artisthistory> GetSeenArtists(string User_ID)
        {
            return _TrackerDAO.GetSeenArtists(User_ID);
        }

        public tbl_artisthistory GetSeenArtistDetails(int ArtistHistory_ID)
        {
            return _TrackerDAO.GetSeenArtistDetails(ArtistHistory_ID);
        }

        public tbl_artisthistory FindSeenArtistEntry(int Lineup_ID, int Event_ID, int Artist_ID, string User_ID)
        {
            return _TrackerDAO.FindSeenArtistEntry(Lineup_ID, Event_ID, Artist_ID, User_ID);
        }

        public void DeleteFromSeenArtists(tbl_artisthistory _entry)
        {
            _TrackerDAO.DeleteFromSeenArtists(_entry);
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

        public void EditArtist(tbl_artists _artists)
        {
            _TrackerDAO.EditArtist(_artists);
        }

        public void AddArtistImage(tbl_artistImages image)
        {
            _TrackerDAO.AddArtistImage(image);
        }

        public void EditArtistImage(tbl_artistImages oldImage, tbl_artistImages newImage)
        {
            _TrackerDAO.EditArtistImage(oldImage, newImage);
        }

        public void RemoveArtistImage(tbl_artistImages image)
        {
            _TrackerDAO.RemoveArtistImage(image);
        }

        //Retrieve List of events that an artist has played at
        public IList<tbl_eventlineup> GetAnArtistsEventHistory(int Artist_ID)
        {
            return _TrackerDAO.GetAnArtistsEventHistory(Artist_ID);
        }

        //-------------------------------------------------------------------------------
        // USER/ARTIST RELATED FUNCTIONS
        public IList<tbl_artisthistory> GetSeenArtistHistory(string User_ID, int Artist_ID)
        {
            return _TrackerDAO.GetSeenArtistHistory(User_ID, Artist_ID);
        }

        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.

        //-------------------------------------------------------------------------------
        // VENUE RELATED FUNCTIONS
        public IList<tbl_venues> GetVenues()
        {
            return _TrackerDAO.GetVenues();
        }

        public IList<tbl_venues> GetVenuesByCountry(int Country_ID)
        {
            return _TrackerDAO.GetVenuesByCountry(Country_ID);
        }

        public void CreateVenue(tbl_venues _venue)
        {
            _TrackerDAO.CreateVenue(_venue);
        }

        public Tracker.Data.tbl_venues GetVenueDetails(int Venue_ID)
        {
            return _TrackerDAO.GetVenueDetails(Venue_ID);
        }

        public void EditVenue(tbl_venues _venues)
        {
            _TrackerDAO.EditVenue(_venues);
        }

        public void DeleteVenue(tbl_venues venue)
        {
            _TrackerDAO.DeleteVenue(venue);
        }

        //-----------------------------------------------------------------------------------------------------------------------
        // Country RELATED FUNCTIONS
        //Gets a list of all the countries within the database.
        public IList<Tracker.Data.tbl_countries> GetCountries()
        {
            return _TrackerDAO.GetCountries();
        }

        //-------------------------------------------------------------------------------
        // File RELATED FUNCTIONS

        public tbl_artistImages GetImageId(int Artist_ID)
        {
            return _TrackerDAO.GetImageId(Artist_ID);
        }

        public tbl_eventImages GetEventImageId(int Event_ID)
        {
            return _TrackerDAO.GetEventImageId(Event_ID);
        }
    }
}