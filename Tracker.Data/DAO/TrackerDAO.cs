using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Data.IDAO;

namespace Tracker.Data.DAO
{
    public class TrackerDAO : ITrackerDAO
    {
        private readonly TrackerEntities _context;

        public TrackerDAO()
        {
            _context = new TrackerEntities();
        }

        //EVENT RELATED FUNCTIONS
        //Get a list of all the events within the database.
        public IList<tbl_events> GetEvents()
        {
            IQueryable<tbl_events> _events;
            _events = from tbl_events in _context.tbl_events select tbl_events;
            return _events.OrderByDescending(x => x.Event_Date).ToList<tbl_events>();
        }

        //Returns the details of a specific event.
        public tbl_events GetEventDetails(int Event_ID)
        {
            IQueryable<tbl_events> _events;
            _events = from tbl_events in _context.tbl_events where tbl_events.Event_ID == Event_ID select tbl_events;
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
            _event.Event_Cancelled = _events.Event_Cancelled;
            _context.SaveChanges();
        }

        //Returns a list of an events lineup.
        public IList<tbl_eventlineup> GetLineUp(int Event_ID)
        {
            IQueryable<tbl_eventlineup> _lineup;
            _lineup = from tbl_eventlineup in _context.tbl_eventlineup where tbl_eventlineup.Event_ID == Event_ID select tbl_eventlineup;
            return _lineup.ToList<tbl_eventlineup>();
        }

        //Allows artists to be added to the lineup
        public void AddToLineup(tbl_eventlineup _lineup)
        {
            _context.tbl_eventlineup.Add(_lineup);
            _context.SaveChanges();
        }

        //Returns the details of a specific lineup entry
        public tbl_eventlineup GetLineupDetails(int Lineup_ID)
        {
            IQueryable<tbl_eventlineup> _entry;
            _entry = from tbl_eventlineup in _context.tbl_eventlineup where tbl_eventlineup.Lineup_ID == Lineup_ID select tbl_eventlineup;
            return _entry.First<tbl_eventlineup>();
        }

        //Removes an artist from a specific events lineup
        public void DeleteFromLineup(tbl_eventlineup _lineup)
        {
            _context.tbl_eventlineup.Remove(_lineup);
            _context.SaveChanges();
        }

        //-------------------------------------------------------------------------------
        //USER EVENT RELATED FUNCTIONS
        //Used to select the Event History of a specific user. Selects the Event_ID from tbl_usereventhistory, which is then to be used to display the records from tbl_events.
        public IList<tbl_eventhistory> GetUserEvents(string User_ID)
        {
            IQueryable<tbl_eventhistory> _eventHistory;
            _eventHistory = from tbl_eventhistory in _context.tbl_eventhistory where tbl_eventhistory.User_ID.Equals(User_ID) select tbl_eventhistory;
            return _eventHistory.ToList<tbl_eventhistory>();
        }

        //Allows a user to add an event to their own event history
        public void AddToUser(tbl_eventhistory _event)
        {
            _context.tbl_eventhistory.Add(_event);
            _context.SaveChanges();
        }

        //Retrieves a list of artists on the lineup of an event within a users event history
        public IList<tbl_artisthistory> GetHistoryLineup(int EventLineup_ID)
        {
            IQueryable<tbl_artisthistory> _eventLineup;
            _eventLineup = from tbl_artisthistory in _context.tbl_artisthistory where tbl_artisthistory.EventLineup_ID == EventLineup_ID select tbl_artisthistory;
            return _eventLineup.ToList<tbl_artisthistory>();
        }

        //Retrieves the details of an event within the users event history
        public tbl_eventhistory GetEventHistoryDetails(int History_ID)
        {
            IQueryable<tbl_eventhistory> _event;
            _event = from tbl_eventhistory in _context.tbl_eventhistory where tbl_eventhistory.History_ID == History_ID select tbl_eventhistory;
            return _event.First<tbl_eventhistory>();
        }

        //Allows a user to remove an event from their event history
        public void DeleteFromUserHistory(tbl_eventhistory _event)
        {
            _context.tbl_eventhistory.Remove(_event);
            _context.SaveChanges();
        }

        //Returns a list of an events lineup through the users events.
        public IList<tbl_eventlineup> GetUsersLineUp(int Event_ID)
        {
            IQueryable<tbl_eventlineup> _lineup;
            _lineup = from tbl_eventlineup in _context.tbl_eventlineup where tbl_eventlineup.Event_ID == Event_ID select tbl_eventlineup;
            return _lineup.ToList<tbl_eventlineup>();
        }

        public void AddToArtistHistory(tbl_artisthistory _entry)
        {
            _context.tbl_artisthistory.Add(_entry);
            _context.SaveChanges();
        }

        public IList<tbl_artisthistory> GetSeenArtists(string User_ID)
        {
            IQueryable<tbl_artisthistory> _artists;
            _artists = from tbl_artisthistory in _context.tbl_artisthistory where tbl_artisthistory.User_ID.Equals(User_ID) select tbl_artisthistory;
            return _artists.ToList<tbl_artisthistory>();
        }

        public tbl_artisthistory GetSeenArtistDetails(int ArtistHistory_ID)
        {
            IQueryable<tbl_artisthistory> _entry;
            _entry = from tbl_artisthistory in _context.tbl_artisthistory where tbl_artisthistory.ArtistHistory_ID == ArtistHistory_ID select tbl_artisthistory;
            return _entry.First<tbl_artisthistory>();
        }

        public tbl_artisthistory FindSeenArtistEntry(int Lineup_ID, int Event_ID, int Artist_ID, string User_ID)
        {
            IQueryable<tbl_artisthistory> _entry;
            _entry = from tbl_artisthistory in _context.tbl_artisthistory where tbl_artisthistory.User_ID.Equals(User_ID) & tbl_artisthistory.Event_ID == Event_ID & tbl_artisthistory.EventLineup_ID == Lineup_ID & tbl_artisthistory.Artist_ID == Artist_ID select tbl_artisthistory;
            return _entry.First<tbl_artisthistory>();
        }

        public void DeleteFromSeenArtists(tbl_artisthistory _entry)
        {
            _context.tbl_artisthistory.Remove(_entry);
            _context.SaveChanges();
        }

        //-------------------------------------------------------------------------------
        // ARTIST RELATED FUNCTIONS
        //Gets a list of all of the artists within database.
        public IList<tbl_artists> GetArtists()
        {
            IQueryable<tbl_artists> _artists;
            _artists = from tbl_artists in _context.tbl_artists select tbl_artists;
            return _artists.OrderBy(x => x.Artist_Name).ToList<tbl_artists>();
        }

        //Retrieves details of a specified artist
        public tbl_artists GetArtistDetails(int Artist_ID)
        {
            IQueryable<tbl_artists> _artist;
            _artist = from tbl_artists in _context.tbl_artists where tbl_artists.Artist_ID == Artist_ID select tbl_artists;
            return _artist.First<tbl_artists>();
        }

        //Inserts a new artist into the database
        public void NewArtist(tbl_artists _artist)
        {
            _context.tbl_artists.Add(_artist);
            _context.SaveChanges();
        }

        //Edits the details of a specific artist.
        public void EditArtist(tbl_artists _artists)
        {
            tbl_artists _artist = GetArtistDetails(_artists.Artist_ID);

            _artist.Artist_ID = _artists.Artist_ID;
            _artist.Artist_Name = _artists.Artist_Name;
            _context.SaveChanges();
        }

        public void AddArtistImage(tbl_artistImages image)
        {
            _context.tbl_artistImages.Add(image);
            _context.SaveChanges();
        }

        public void EditArtistImage(tbl_artistImages oldImage, tbl_artistImages newImage)
        {
            tbl_artistImages imageEntry = _context.tbl_artistImages.FirstOrDefault(s => s.File_ID == oldImage.File_ID);
            _context.tbl_artistImages.Remove(imageEntry);
            imageEntry.Content = newImage.Content;
            imageEntry.Content_Type = newImage.Content_Type;
            imageEntry.File_Name = newImage.File_Name;
            _context.tbl_artistImages.Add(newImage);
            _context.SaveChanges();
        }

        public void RemoveArtistImage(tbl_artistImages image)
        {
            _context.tbl_artistImages.Remove(image);
            _context.SaveChanges();
        }

        public IList<tbl_eventlineup> GetAnArtistsEventHistory(int Artist_ID)
        {
            IQueryable<tbl_eventlineup> _events;
            _events = from tbl_eventlineup in _context.tbl_eventlineup where tbl_eventlineup.Artist_ID == Artist_ID select tbl_eventlineup;
            return _events.OrderBy(x => x.tbl_events.Event_Date).ToList<tbl_eventlineup>();
        }

        //-------------------------------------------------------------------------------
        // USER/ARTIST RELATED FUNCTIONS
        public IList<tbl_artisthistory> GetSeenArtistHistory(string User_ID, int Artist_ID)
        {
            IQueryable<tbl_artisthistory> _UsersArtistEvents;
            _UsersArtistEvents = from tbl_artisthistory in _context.tbl_artisthistory where tbl_artisthistory.User_ID.Equals(User_ID) & tbl_artisthistory.Artist_ID == Artist_ID select tbl_artisthistory;
            return _UsersArtistEvents.ToList<tbl_artisthistory>();
        }

        //-------------------------------------------------------------------------------
        // USER RELATED FUNCTIONS
        //Gets a list of all the users within the database.

        //-------------------------------------------------------------------------------
        // VENUE RELATED FUNCTIONS

        public IList<tbl_venues> GetVenues()
        {
            IQueryable<tbl_venues> _Venues;
            _Venues = from tbl_venues in _context.tbl_venues select tbl_venues;
            return _Venues.OrderBy(i => i.V_City).ThenBy(n => n.V_Name).ToList<tbl_venues>();
        }

        public IList<tbl_venues> GetVenuesByCountry(int Country_ID)
        {
            IQueryable<tbl_venues> _Venues;
            _Venues = from tbl_venues in _context.tbl_venues where tbl_venues.V_Country == Country_ID select tbl_venues;
            return _Venues.OrderBy(i => i.V_City).ThenBy(n => n.V_Name).ToList<tbl_venues>();
        }

        public void CreateVenue(tbl_venues _venue)
        {
            _context.tbl_venues.Add(_venue);
            _context.SaveChanges();
        }

        public tbl_venues GetVenueDetails(int Venue_ID)
        {
            IQueryable<tbl_venues> _venues;
            _venues = from tbl_venues in _context.tbl_venues where tbl_venues.Venue_ID == Venue_ID select tbl_venues;
            return _venues.First<tbl_venues>();
        }

        public void EditVenue(tbl_venues _venues)
        {
            tbl_venues _venue = GetVenueDetails(_venues.Venue_ID);

            _venue.V_Name = _venues.V_Name;
            _venue.V_StreetAddress = _venues.V_StreetAddress;
            _venue.V_City = _venues.V_City;
            _venue.V_Postcode = _venues.V_Postcode;
            _venue.V_Country = _venues.V_Country;

            _context.SaveChanges();
        }

        public void DeleteVenue(tbl_venues venue)
        {
            _context.tbl_venues.Remove(venue);
            _context.SaveChanges();
        }

        //-------------------------------------------------------------------------------
        // Country RELATED FUNCTIONS

        public IList<tbl_countries> GetCountries()
        {
            IQueryable<tbl_countries> _Countries;
            _Countries = from tbl_countries in _context.tbl_countries select tbl_countries;
            return _Countries.ToList<tbl_countries>();
        }

        //-------------------------------------------------------------------------------
        // File RELATED FUNCTIONS

        public tbl_artistImages GetImageId(int Artist_ID)
        {
            IQueryable<tbl_artistImages> _file = from tbl_artistImages in _context.tbl_artistImages where tbl_artistImages.Artist_ID == Artist_ID select tbl_artistImages;
            return _file.First<tbl_artistImages>();
        }
    }
}