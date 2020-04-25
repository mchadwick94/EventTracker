using EventTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
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
            if (Artist_ID == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            tbl_artists _artist = _context.tbl_artists.Include(s => s.tbl_artistImages).SingleOrDefault(s => s.Artist_ID == Artist_ID); //fetches all files associated with the artist regardless of type.
            if (_artist == null)
            {
                return HttpNotFound();
            }
            return View(_trackerService.GetArtistDetails(Artist_ID));
        }

        // Inserts a new artist into the database.
        [HttpGet]
        public ActionResult NewArtist()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewArtist(tbl_artists _artist, HttpPostedFileBase upload)
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

                //====================== THIS BLOCK IS REGARDING ADDING IMAGES TO ARTIST
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new Tracker.Data.tbl_artistImages
                    {
                        File_Name = System.IO.Path.GetFileName(upload.FileName),
                        Content_Type = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    _artist.tbl_artistImages = new List<tbl_artistImages> { avatar };
                }
                //=========================
                _trackerService.NewArtist(_artist); //Else add a new artist to the database with the Artist_Name given in the form.
                return RedirectToAction("NewArtist");
            }
            else
            {
                return View();
            }
        }

        //Edit an artists details
        [HttpGet] //Retrieves the details of the event being edited
        public ActionResult EditArtist(int Artist_ID)
        {
            if (Artist_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_artists _artist = _context.tbl_artists.Include(s => s.tbl_artistImages).SingleOrDefault(s => s.Artist_ID == Artist_ID); //fetches all files associated with the artist regardless of type.
            if (_artist == null)
            {
                return HttpNotFound("An Artist wasn't entered.");
            }
            return View(_trackerService.GetArtistDetails(Artist_ID));
        }

        [HttpPost] //Posts the new variables into the database at the specific event being edited
        public ActionResult EditArtist(int Artist_ID, tbl_artists _artist, HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    if (_artist.tbl_artistImages.Any())
                    {
                        _context.tbl_artistImages.Remove(_artist.tbl_artistImages.First());
                    }
                    var avatar = new Tracker.Data.tbl_artistImages
                    {
                        File_Name = System.IO.Path.GetFileName(upload.FileName),
                        Content_Type = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    _artist.tbl_artistImages = new List<tbl_artistImages> { avatar };
                }

                var doesArtistExist = _context.tbl_artists.Any(x => x.Artist_Name == _artist.Artist_Name); //Creates a variable which is assigned the value of any artist in tbl_artists matching the Artist_Name given in the form (_artist)
                if (doesArtistExist) //If there is a value assigned to the variable
                {
                    ModelState.AddModelError("Artist_Name", "This Artist already exists"); //throw an error
                    return View();
                }
                _trackerService.EditArtist(_artist);
                return RedirectToAction("GetArtistDetails", new { _artist.Artist_ID });
            }
            catch
            {
                return View(_trackerService.GetArtistDetails(Artist_ID));
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

        public ActionResult GetSeenArtists(int User_ID) //Returns indexed view of all artists a user has seen
        {
            return View(_trackerService.GetSeenArtists(User_ID));
        }

        public ActionResult GetSeenArtistDetails(int ArtistHistory_ID) //Retrieves the details of an entry in a users artist history
        {
            return View(_trackerService.GetSeenArtistDetails(ArtistHistory_ID));
        }

        public ActionResult FindSeenArtistEntry(int Lineup_ID, int Event_ID, int Artist_ID, int User_ID)
        {
            return View(_trackerService.FindSeenArtistEntry(Lineup_ID, Event_ID, Artist_ID, User_ID));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //removes an artist from a users seen history
        public ActionResult DeleteFromSeenArtists(tbl_artisthistory _entry, tbl_eventlineup _lineup, tbl_events _event, int Lineup_ID, int Event_ID, int Artist_ID, int User_ID)
        {
            try
            {
                _event = _trackerService.GetEventDetails(_entry.Event_ID);
                string Event_Name = _event.Event_Name;
                _entry = _trackerService.FindSeenArtistEntry(Lineup_ID, Event_ID, Artist_ID, User_ID);
                _trackerService.DeleteFromSeenArtists(_entry);
                _lineup = _trackerService.GetLineupDetails(_entry.EventLineup_ID);
                return RedirectToAction("GetUsersLineup", "Event", new { _lineup.Event_ID, Event_Name });
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
                return RedirectToAction("GetUsersLineup", "Event", new { _lineup.Event_ID, Event_Name });
            }
            catch
            {
                return RedirectToAction("GetUsersLineup", "Event", new { _lineup.Event_ID });
            }
        }

        public ActionResult GetSeenArtistHistory(int User_ID, int Artist_ID)
        {
            ViewBag.Artist_Name = _trackerService.GetArtistDetails(Artist_ID).Artist_Name.ToString();
            return View(_trackerService.GetSeenArtistHistory(User_ID, Artist_ID));
        }
    }
}