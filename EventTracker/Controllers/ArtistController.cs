using EventTracker.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI.DataVisualization.Charting;
using Tracker.Data;

namespace EventTracker.Controllers
    {
    public class ArtistController : ApplicationController
        {
        //private Tracker.Services.IService.ITrackerService _trackerService;
        private TrackerEntities _context;

        public string User_ID;

        public ArtistController()
            {
            User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<string> UsersEvents = new List<string>();
            foreach (var item in _trackerService.GetUserEvents(User_ID))
                {
                UsersEvents.Add(item.Event_ID.ToString());
                }
            ViewBag.MyEvents = UsersEvents;

            _context = new TrackerEntities();
            }

        public ActionResult FilterArtistEvents()
            {
            return View();
            }

        // Retrieves a list of all the artists within the database (tbl_artists)
        public ActionResult GetArtists()
            {
            if (HttpContext.User.Identity.IsAuthenticated != false)
                {
                User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                }

            //return View(_trackerService.GetArtists());
            return View(ViewBag.Artists);
            }

        // Retrieves the details of a specific artist
        public ActionResult GetArtistDetails(int Artist_ID)
            {
            tbl_artists _artist = _context.tbl_artists.Include(s => s.tbl_artistImages).SingleOrDefault(s => s.Artist_ID == Artist_ID); //fetches all files associated with the artist regardless of type.
            if (_artist == null)
                {
                return HttpNotFound();
                }
            return View(_trackerService.GetArtistDetails(Artist_ID));
            }

        //SQL Action to return a grouped list of artists based on how many times a user has seen them
        [WebMethod]
        public ActionResult GetUsersArtistCount(string User_ID)
            {
            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";
            User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();

            SqlConnection sqlConnection = new SqlConnection(connString);
            SqlCommand MySqlCmd = sqlConnection.CreateCommand();
            SqlDataReader adapter;
            MySqlCmd.CommandText = @"	USE [EventTracker] select [db_eventtracker].[tbl_artists].[Artist_ID], [db_eventtracker].[tbl_artists].[Artist_Name], count(*) AS c from[db_eventtracker].[tbl_artists] left join[db_eventtracker].[tbl_artisthistory] on[db_eventtracker].[tbl_artists].Artist_ID = [db_eventtracker].[tbl_artisthistory].[Artist_ID] where[db_eventtracker].[tbl_artisthistory].[User_ID] = '" + User_ID + "' group by[db_eventtracker].[tbl_artists].[Artist_ID], [db_eventtracker].[tbl_artists].[Artist_Name] order by C DESC;";
            sqlConnection.Open();
            DataTable dt = new DataTable("countTable");
            adapter = MySqlCmd.ExecuteReader();
            dt.Load(adapter);
            sqlConnection.Close();

            //Convert DataTable to List
            List<SeenArtistCount> artistCountList = dt.AsEnumerable().Select(m => new SeenArtistCount()
                {
                Artist_ID = m.Field<int>("Artist_ID"),
                Artist_Name = m.Field<string>("Artist_Name"),
                c = m.Field<int>("c"),
                }).ToList();

            return View(artistCountList);
            }

        // Retrieves the lineup for an event inside a specific users event history
        public ActionResult GetHistoryLineup(int EventLineup_ID)
            {
            return View(_trackerService.GetHistoryLineup(EventLineup_ID));
            }

        public ActionResult GetSeenArtists(string User_ID) //Returns indexed view of all artists a user has seen
            {
            return View(_trackerService.GetSeenArtists(User_ID));
            }

        public ActionResult GetSeenArtistDetails(int ArtistHistory_ID) //Retrieves the details of an entry in a users artist history
            {
            return View(_trackerService.GetSeenArtistDetails(ArtistHistory_ID));
            }

        public ActionResult FindSeenArtistEntry(int Lineup_ID, int Event_ID, int Artist_ID, string User_ID)
            {
            return View(_trackerService.FindSeenArtistEntry(Lineup_ID, Event_ID, Artist_ID, User_ID));
            }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //removes an artist from a users seen history
        public ActionResult DeleteFromSeenArtists(tbl_artisthistory _entry, tbl_eventlineup _lineup, tbl_events _event, int Lineup_ID, int Event_ID, int Artist_ID, string User_ID)
            {
            try
                {
                _event = _trackerService.GetEventDetails(_entry.Event_ID);
                string Event_Name = _event.Event_Name;
                _entry = _trackerService.FindSeenArtistEntry(Lineup_ID, Event_ID, Artist_ID, User_ID);
                _trackerService.DeleteFromSeenArtists(_entry);
                _lineup = _trackerService.GetLineupDetails(_entry.EventLineup_ID);
                return RedirectToAction("ReturnPreviousPage", new { controller = "Application" });
                }
            catch
                {
                return View();
                }
            }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //TO THIS METHOD, ADD VERIFICATION AND IF ALREADY EXISTS, REMOVE FROM DATABASE
        public ActionResult AddToArtistHistory(tbl_artisthistory _entry, tbl_eventlineup _lineup, tbl_events _event)
            {
            try
                {
                _event = _trackerService.GetEventDetails(_entry.Event_ID);
                string Event_Name = _event.Event_Name;
                _lineup = _trackerService.GetLineupDetails(_entry.EventLineup_ID);
                _trackerService.AddToArtistHistory(_entry);
                return RedirectToAction("ReturnPreviousPage", new { controller = "Application" });
                }
            catch
                {
                return RedirectToAction("ReturnPreviousPage", new { controller = "Application" });
                }
            }

        public ActionResult GetSeenArtistHistory(string User_ID, int Artist_ID)
            {
            ViewBag.Artist_Name = _trackerService.GetArtistDetails(Artist_ID).Artist_Name.ToString();
            return View(_trackerService.GetSeenArtistHistory(User_ID, Artist_ID));
            }
        }
    }