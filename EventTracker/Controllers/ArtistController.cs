using System;
using System.Linq;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
{
    public class ArtistController : ApplicationController
    {
        //private Tracker.Services.IService.ITrackerService _trackerService;
        private TrackerEntities _context;
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

        // Retrieves the lineup for an event inside a specific users event history
        public ActionResult GetHistoryLineup(int EventLineup_ID)
        {
            return View(_trackerService.GetHistoryLineup(EventLineup_ID));
        }
    }
}
