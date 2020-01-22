using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Data;
using Tracker.Data.IDAO;
using Tracker.Data.DAO;

namespace Tracker.Services.Service
{
    public class TrackerService : Tracker.Services.IService.ITrackerService
    {
        private ITrackerDAO _TrackerDAO;

        public TrackerService()
        {
            _TrackerDAO = new TrackerDAO();
        }

        public IList<tbl_events> GetEvents()
        {
            return _TrackerDAO.GetEvents();
        }

        public IList<tbl_eventhistory> GetUserEvents(int User_ID)
        {
            return _TrackerDAO.GetUserEvents(User_ID);
        }

        public IList<tbl_artists> GetArtists()
        {
            return _TrackerDAO.GetArtists();
        }
        public IList<tbl_users> GetUsers()
        {
            return _TrackerDAO.GetUsers();
        }

        
    }
}
