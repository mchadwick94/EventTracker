using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Services.IService
{
    public interface ITrackerService
    {
        IList<Tracker.Data.tbl_events> GetEvents();
        IList<Tracker.Data.tbl_artists> GetArtists();
        IList<Tracker.Data.tbl_users> GetUsers();
    }
}
