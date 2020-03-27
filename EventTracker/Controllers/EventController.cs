using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
{
    public class EventController : ApplicationController
    {
        public int _eventID;
        public string User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId().GetHashCode().ToString();


        public EventController()
        {
            ViewBag.Lineup = _trackerService.GetLineUp(_eventID);
            ViewBag.MyEvents = _trackerService.GetUserEvents(User_ID);
        }
        // GET: Complete Event List
        public ActionResult GetEvents()
        {
            List<string> UsersEvents = new List<string>();
            foreach (var item in _trackerService.GetUserEvents(User_ID))
            {
                UsersEvents.Add(item.Event_ID.ToString());
            }
            ViewBag.MyEvents = _trackerService.GetUserEvents(User_ID);
            ViewBag.MyEvents = UsersEvents;
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
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Allows the Get and Post action to be done immediately to avoid redirecting the user to a 'Create' page.
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

        //Edits the details of an event
        [HttpGet] //Retrieves the details of the event being edited
        public ActionResult EditEvent(int Event_ID)
        {
            return View(_trackerService.GetEventDetails(Event_ID));
        }
        [HttpPost] //Posts the new variables into the database at the specific event being edited
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
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Allows events to be deleted without the user having to confirm it on a different page.
        public ActionResult DeleteFromUserHistory(int History_ID, tbl_eventhistory _event)
        {
            try
            {
                _event = _trackerService.GetEventHistoryDetails(_event.History_ID);
                _trackerService.DeleteFromUserHistory(_event);
                return RedirectToAction("GetUserEvents", new { controller = "Event", User_ID = Convert.ToString(User.Identity.GetUserId()).GetHashCode() });
            }
            catch
            {
                return View(_trackerService.GetEventHistoryDetails(History_ID));
            }
        }

        //Get an events Lineup through users events page
        public ActionResult GetUsersLineUp(int Event_ID, tbl_events _event)
        {
            List<string> UsersSeenLineup= new List<string>();
            _event = _trackerService.GetEventDetails(Event_ID);
            foreach (var item in _trackerService.GetSeenArtists(Convert.ToInt32(User_ID))){
                if(item.Event_ID == _event.Event_ID)
                {
                    UsersSeenLineup.Add(item.Artist_ID.ToString());
                }
            }
            ViewBag.UsersSeenOnLineup = UsersSeenLineup;
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
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Allows the user to add to a lineup without being redirected to a create page.
        public ActionResult AddToLineup(int Event_ID, tbl_eventlineup _lineup)
        {
            try
            {
                _trackerService.AddToLineup(_lineup);
                return RedirectToAction("GetLineup", new { _lineup.Event_ID });
            }
            catch
            {
                return View(_trackerService.GetLineUp(_lineup.Event_ID));
            }
        }

        //Retrieves the details of a specific events lineup.
        public ActionResult GetLineupDetails(int Lineup_ID)
        {
            return View(_trackerService.GetLineupDetails(Lineup_ID));
        }

        /*[HttpGet] //Allows the user to delete an artist from an events lineup
        public ActionResult deleteFromLineup(int Lineup_ID)
        {
            return View(_trackerService.GetLineupDetails(Lineup_ID));
        }*/
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult DeleteFromLineup(tbl_eventlineup _lineup)
        {
            try
            {
                _lineup = _trackerService.GetLineupDetails(_lineup.Lineup_ID);
                _trackerService.DeleteFromLineup(_lineup);
                return RedirectToAction("GetLineup", new { controller = "Event", _lineup.Event_ID });
            }
            catch
            {
                return View();
            }
        }
    }
}


