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
    public class EventController : ApplicationController
    {
        public int _eventID;

        //private Tracker.Services.IService.ITrackerService _trackerService;

        public EventController()
        {
            //_trackerService = new Tracker.Services.Service.TrackerService();
            
            ViewBag.Lineup = _trackerService.GetLineUp(_eventID);
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


        //ADD EVENT TO A USERS HISTORY
       /* [HttpGet]
        public ActionResult AddToUser()
        {
            return View();
        }*/
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult AddToUser(tbl_eventhistory _event)
        { 
            
            try
            {

                _trackerService.AddToUser(_event);
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
        public ActionResult GetUserEvents(string User_ID)
        {
            return View(_trackerService.GetUserEvents(User_ID));
        }

        public ActionResult GetEventHistoryDetails(int Event_ID)
        {
            return View(_trackerService.GetEventHistoryDetails(Event_ID));
        }

        //DELETES an event from a users event history
        /*[HttpGet]
        public ActionResult deleteFromUserHistory(int History_ID)
        {
            return View(_trackerService.GetEventHistoryDetails(History_ID));
        }*/
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
//[HttpPost]
        public ActionResult deleteFromUserHistory(int History_ID, tbl_eventhistory _event)
        {
            try
            {
                _event = _trackerService.GetEventHistoryDetails(_event.History_ID);
                _trackerService.deleteFromUserHistory(_event);
                return RedirectToAction("GetUserEvents", new { controller = "Event", User_ID = Convert.ToString(User.Identity.GetUserId()).GetHashCode() });
            }
            catch
            {
                return View(_trackerService.GetEventHistoryDetails(History_ID));
            }
        }




        //Get an events Lineup through users events page
        public ActionResult GetUsersLineUp(int Event_ID)
        {
            _eventID = Event_ID;
            return View(_trackerService.GetLineUp(Event_ID));

        }








        //Get an events Lineup
        public ActionResult GetLineUp(int Event_ID)
        {
            _eventID = Event_ID;
            return View(_trackerService.GetLineUp(Event_ID));
            
        }

        //Adds to an events lineup
        /*[HttpGet]
        public ActionResult addToLineup()
        {
            List<SelectListItem> artistList = new List<SelectListItem>();
            foreach (var item in _trackerService.GetArtists())
            {
                artistList.Add(new SelectListItem()
                {
                    Text = item.Artist_Name,
                    Value = item.Artist_ID.ToString(),
                    Selected = (item.Artist_Name == (selectedArtist) ? true : false)
                });
            }
            ViewBag.artistList = artistList;
            return View();
        }*/
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult addToLineup(int Event_ID, tbl_eventlineup _lineup)
        {
            try
            {
                _trackerService.addToLineup(_lineup);
                return RedirectToAction("GetLineup", new { Event_ID = _lineup.Event_ID });
            }
            catch
            {
                return View(_trackerService.GetLineUp(_lineup.Event_ID));
            }
        }

        public ActionResult GetLineupDetails(int Lineup_ID)
        {
            return View(_trackerService.GetLineupDetails(Lineup_ID));
        }


        /*[HttpGet]
        public ActionResult deleteFromLineup(int Lineup_ID)
        {
            return View(_trackerService.GetLineupDetails(Lineup_ID));
        }*/
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult deleteFromLineup(tbl_eventlineup _lineup)
        {
            try
            {
                _lineup = _trackerService.GetLineupDetails(_lineup.Lineup_ID);
                _trackerService.deleteFromLineup(_lineup);
                return RedirectToAction("GetLineup", new { controller = "Event", Event_ID = _lineup.Event_ID  });
            }
            catch
            {
                return View();
            }
        }   

            }
        }


