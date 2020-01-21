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
    public class GuideLogic
    {
        private readonly IDocumentDbRepository _ddb;

        public GuideLogic(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task<List<Guide>> Get()
        {
            GuideDataAccess guidDataAccess = new GuideDataAccess(_ddb);
            var result = await guidDataAccess.GetAllItemsAsync();
            return result.ToList();
        }

        public async Task<Guide> Get(string id)
        {
            GuideDataAccess guidDataAccess = new GuideDataAccess(_ddb);
            return await guidDataAccess.GetItemByIdAsync(id);
        }

        public async Task<(Guide, Team)> GetGuideAndTeam(string guideId)
        {
            TeamLogic teamLogic = new TeamLogic(_ddb);
            Team t = null;
            Guide g;
            if ((g = await Get(guideId)) != null)
            {
                t = await teamLogic.Get(g.TeamId);
            }

            return (g, t);
        }

        public async Task<Guide> Add(Guide guide)
        {
            GuideDataAccess guidDataAccess = new GuideDataAccess(_ddb);
            return await guidDataAccess.CreateItemAsync(guide);
        }

        public async Task<List<Guide>> Add(List<Guide> guides)
        {
            GuideDataAccess guidDataAccess = new GuideDataAccess(_ddb);
            return await guidDataAccess.CreateItemsAsync(guides);
        }

        public async Task<Guide> Update(Guide guide)
        {
            GuideDataAccess guidDataAccess = new GuideDataAccess(_ddb);
            return await guidDataAccess.UpdateItemAsync(guide);
        }

        public async Task<bool> Delete(string id)
        {
            GuideDataAccess guidDataAccess = new GuideDataAccess(_ddb);
            return await guidDataAccess.DeleteItemAsync(id);
        }
    }
}
