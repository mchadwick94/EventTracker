using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
            //_trackerService = new Tracker.Services.Service.TrackerService();
            _context = new TrackerEntities();
        }
        // GET: Artist
        public ActionResult GetArtists()
        {
            //return View(_trackerService.GetArtists());
            return View(ViewBag.Artists);
        }

        // GET: Artist/Details/5
        public ActionResult GetArtistDetails(int Artist_ID)
        {
            return View(_trackerService.GetArtistDetails(Artist_ID));
        }

        [HttpGet]
        public ActionResult NewArtist()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewArtist(tbl_artists _artist)
        {
            if (String.IsNullOrEmpty(_artist.Artist_Name))
            {
                ModelState.AddModelError("Artist_Name", "Need an Artists Name"); 
            }
            if (ModelState.IsValid)
            {
                var doesArtistExist = _context.tbl_artists.Any(x => x.Artist_Name == _artist.Artist_Name);
                if (doesArtistExist)
                {
                    ModelState.AddModelError("Artist_Name", "This Artist already exists");
                    return View();
                }
                _trackerService.NewArtist(_artist);
                return RedirectToAction("NewArtist");
                
                
            }
            else
            {

                return View();
            }
        }
    }
}
