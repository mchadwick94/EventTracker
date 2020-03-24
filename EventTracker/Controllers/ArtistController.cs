using EventTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using Tracker.Data;

namespace EventTracker.Controllers
{
    public class ArtistController : ApplicationController
    {
        //private Tracker.Services.IService.ITrackerService _trackerService;
        private TrackerEntities _context;
        public string User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId().GetHashCode().ToString();

        public ArtistController()
        {
            _context = new TrackerEntities();

        }
        // Retrieves a list of all the artists within the database (tbl_artists)
        public ActionResult GetArtists()
        {
            //return View(_trackerService.GetArtists());
            return View(ViewBag.Artists);
        }

        // Retrieves the details of a specific artist
        public ActionResult GetArtistDetails(int Artist_ID)
        {
            return View(_trackerService.GetArtistDetails(Artist_ID));
        }

        // Inserts a new artist into the database.
        [HttpGet]
        public ActionResult NewArtist()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewArtist(tbl_artists _artist)
        {
            if (String.IsNullOrEmpty(_artist.Artist_Name))//Checks if the field 'Artist_Name' is null, if so, throws an error.
            {
                ModelState.AddModelError("Artist_Name", "Need an Artists Name");
            }
            if (ModelState.IsValid)
            {
                var doesArtistExist = _context.tbl_artists.Any(x => x.Artist_Name == _artist.Artist_Name); //Creates a variable which is assigned the value of any artist in tbl_artists matching the Artist_Name given in the form (_artist)
                if (doesArtistExist) //If there is a value assigned to the variable
                {
                    ModelState.AddModelError("Artist_Name", "This Artist already exists"); //throw an error
                    return View();
                }
                _trackerService.NewArtist(_artist); //Else add a new artist to the database with the Artist_Name given in the form.
                return RedirectToAction("NewArtist");
            }
            else
            {
                return View();
            }
        }

        //SQL Action to return a grouped list of artists based on how many times a user has seen them
        [WebMethod]
        public ActionResult GetUsersArtistCount(int User_ID)
        {
            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";

            SqlConnection MyConn = new SqlConnection(connString);
            SqlCommand MySqlCmd = MyConn.CreateCommand();
            SqlDataReader adapter;
            MySqlCmd.CommandText = @"select [db_eventtracker].[tbl_artists].[Artist_ID], [db_eventtracker].[tbl_artists].[Artist_Name], count(*) AS c from[db_eventtracker].[tbl_artists] left join[db_eventtracker].[tbl_artisthistory] on[db_eventtracker].[tbl_artists].Artist_ID = [db_eventtracker].[tbl_artisthistory].[Artist_ID] where[db_eventtracker].[tbl_artisthistory].[User_ID] = ' " + User_ID + " 'group by[db_eventtracker].[tbl_artists].[Artist_ID], [db_eventtracker].[tbl_artists].[Artist_Name] order by C DESC;";
            MyConn.Open();
            DataTable dt = new DataTable("countTable");
            adapter = MySqlCmd.ExecuteReader();
            dt.Load(adapter);
            MyConn.Close();

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
    }
}
