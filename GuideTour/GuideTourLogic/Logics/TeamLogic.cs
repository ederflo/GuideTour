using GuideTourData.DataAccess;
using GuideTourData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourLogic.Logics
{
    public class TeamLogic
    {
        public List<Team> Get()
        {
            TeamDataAccess tourDataAccess = new TeamDataAccess();
            return tourDataAccess.Get();
        }

        public Team Get(string teamname)
        {
            TeamDataAccess tourDataAccess = new TeamDataAccess();
            return tourDataAccess.Get(teamname);
        }

        public Team Add(Team team)
        {
            TeamDataAccess tourDataAccess = new TeamDataAccess();
            return tourDataAccess.Add(team);
        }

        public Team Update(Team team)
        {
            TeamDataAccess tourDataAccess = new TeamDataAccess();
            return tourDataAccess.Update(team);
        }

        public bool Delete(string teamname)
        {
            TeamDataAccess tourDataAccess = new TeamDataAccess();
            return tourDataAccess.Delete(teamname);
        }
    }
}
