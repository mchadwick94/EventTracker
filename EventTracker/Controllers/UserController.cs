using EventTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EventTracker.Controllers
    {
    public class UserController : Controller
        {
        private Tracker.Services.IService.ITrackerService _trackerService;
        private EventTracker.Models.ApplicationDbContext _AppContext;

        public UserController()
            {
            _trackerService = new Tracker.Services.Service.TrackerService();
            _AppContext = new EventTracker.Models.ApplicationDbContext();
            }

        [HttpGet]
        public ActionResult RoleCreate()
            {
            return View();
            }

        [HttpPost]
        public ActionResult RoleCreate(FormCollection collection)
            {
            try
                {
                _AppContext.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = collection["RoleName"] });
                _AppContext.SaveChanges();
                return RedirectToAction("GetRoles");
                }
            catch
                {
                return View();
                }
            }

        public ActionResult GetRoles()
            {
            return View(_AppContext.Roles.ToList());
            }

        [HttpGet]
        public ActionResult ManageUserRoles()
            {
            var roleList = _AppContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;

            var userList = _AppContext.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;
            return View();
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRoles(string UserName, string RoleName)
            {
            ApplicationUser user = _AppContext.Users.Where(u => u.UserName.Equals(UserName, System.StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var um = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(_AppContext));
            var idResult = um.AddToRole(user.Id, RoleName);
            var roleList = _AppContext.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = roleList;

            var userList = _AppContext.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.UserName.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = userList;
            return View("ManageUserRoles");
            }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetRolesForUser(string UserName)
            { 
            if (!string.IsNullOrWhiteSpace(UserName))
                {
                ApplicationUser user = _AppContext.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var um = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(_AppContext));
                ViewBag.RolesForUser = um.GetRoles(user.Id);
                }
            return View("GetRolesForUserConfirmed");
            }

        public class RetrieveCurrentUser
            {
            public string User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            }
        }
    }