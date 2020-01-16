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

        public async Task<Team> Add(Team team)
        {
            TeamDataAccess teamDataAccess = new TeamDataAccess(_ddb);
            return await teamDataAccess.CreateItemAsync(team);
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
