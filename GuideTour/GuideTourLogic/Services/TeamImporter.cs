using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourLogic.Services
{
    public class TeamImporter
    {
        private readonly IDocumentDbRepository _ddb;

        public TeamImporter(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task<bool> ImportTeams()
        {
            TeamLogic teamLogic = new TeamLogic(_ddb);
            GuideLogic guide = new GuideLogic(_ddb);
            List<ImportTeam> importedTeams = GetImportedTeams();
            List<Team> teams = new List<Team>();
            List<Guide> guides = new List<Guide>();
            bool success = false;

            foreach (ImportTeam t in importedTeams)
            {
                Team curTeam = toTeam(t);
                teams.Add(curTeam);
                foreach (Guide g in t.Guides)
                {
                    g.TeamId = curTeam.Id.ToString();
                    g.Id = ObjectId.GenerateNewId().ToString();
                    guides.Add(g);
                }
            }

            await teamLogic.Add(teams);
            await guide.Add(guides);

            return success;
        }

        private List<ImportTeam> GetImportedTeams()
        {
            return GuideTourData.Services.TeamImporter.LoadJson("./../Teams.json");
        }

        private Team toTeam(ImportTeam t)
        {
            return new Team()
            {
                Name = t.Name,
                Id = ObjectId.GenerateNewId().ToString()
            };
        }
    }
}
