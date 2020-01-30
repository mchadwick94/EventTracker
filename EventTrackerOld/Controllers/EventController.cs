using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
{
    public class EventController : Controller
    {
        private Tracker.Services.IService.ITrackerService _trackerService;

        public EventController()
        {
            _trackerService = new Tracker.Services.Service.TrackerService();

        }
        // GET: Complete Event List
        public ActionResult GetEvents()
        {
            return View(_trackerService.GetEvents());
        }

        //Returns the details of a specific event.
        public ActionResult GetEventDetails(int Event_ID)
        {
            return View(_trackerService.GetEventDetails(Event_ID));
        }

        //CREATE a new event
        [HttpGet]
        public ActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvent(tbl_events _event)
        {
            try
            {
                _trackerService.CreateEvent(_event);
                return RedirectToAction("GetEvents");
            }
            catch
            {
                return View();
            }
        }


        //GET: EDIT AN EVENT
        [HttpGet] //Retrieves the details of the event being edited
        public ActionResult EditEvent(int Event_ID)
        {
            return View(_trackerService.GetEventDetails(Event_ID));
        }
        [HttpPost]
        public ActionResult EditEvent(int Event_ID, tbl_events _event)
        {
            try
            {
                _trackerService.EditEvent(_event);
                return RedirectToAction("GetEvents");
            }
            catch
            {
                return View(_trackerService.GetEventDetails(Event_ID));
            }
        }




        // GET: Displays the full list of events for a specific user.
        public ActionResult GetUserEvents(string Id)
        {
            return View(_trackerService.GetUserEvents(Id));
        }


        //Get an events Lineup
        public ActionResult GetLineUp(int Event_ID)
        {
            return View(_trackerService.GetLineUp(Event_ID));
        }

       






        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
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

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Event/Edit/5
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

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Event/Delete/5
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
