using GuideTourData.DataAccess;
using GuideTourData.Models;
using GuideTourData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourLogic.Logics
{
    public class TeamLogic
    {
        private readonly IDocumentDbRepository _ddb;

        public TeamLogic(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }


        public async Task<List<Team>> Get()
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            var result = await teamDataAccess.GetAllItemsAsync();
            return result.ToList();
        }

        public async Task<Team> Get(string id)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.GetItemByIdAsync(id);
        }

        public async Task<Team> GetByName(string teamname)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.GetItemAsync(x => x.Name == teamname);
        }

        public async Task<List<Guide>> GetAllGuidesByTeamname(string teamname)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            Team t = await GetByName(teamname);
            if (t == null)
                return null;
            return await GetAllGuidesByTeamId(t.Id.ToString());
        }

        public async Task<List<Guide>> GetAllGuidesByTeamId(string teamId)
        {
            GuideDataAccess guideDataAccess = new GuideDataAccess(_ddb);
            var result = await guideDataAccess.GetItemsAsync(x => x.TeamId == teamId);
            return result.ToList();
        }

        public async Task<Team> Add(Team team)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.CreateItemAsync(team);
        }

        public async Task<List<Team>> Add(List<Team> teams)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.CreateItemsAsync(teams);
        }

        public async Task<Team> Update(Team team)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.UpdateItemAsync(team);
        }

        public async Task<bool> Delete(string id)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.DeleteItemAsync(id);
        }
    }
}
