using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace EventTracker.Controllers
{
    public class UserController : Controller
    {
        private Tracker.Services.IService.ITrackerService _trackerService;

        public UserController()
        {
            _trackerService = new Tracker.Services.Service.TrackerService();
        }

        // GET: User - CAN BE REMOVED/REPLACED WITH ACTION TO RETRIEVE USERS IN NEW USER DATABASE
        public ActionResult GetUsers()
        {
            return View(_trackerService.GetUsers());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public class RetrieveCurrentUser
        {
            public string User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId().GetHashCode().ToString();
        }
    }
}