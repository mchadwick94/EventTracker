using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventTracker.Controllers
{
    public class ArtistController : ApplicationController
    {
        //private Tracker.Services.IService.ITrackerService _trackerService;
        public ArtistController()
        {
            //_trackerService = new Tracker.Services.Service.TrackerService();
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

        // GET: Artist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artist/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artist/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
