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

        public async Task ImportTeams()
        {
            TeamLogic teamLogic = new TeamLogic(_ddb);
            GuideLogic guide = new GuideLogic(_ddb);
            List<ImportTeam> importedTeams = GetImportedTeams();
            List<Team> teams = new List<Team>();
            List<Guide> guides = new List<Guide>();

            foreach (ImportTeam t in importedTeams)
            {
                Team curTeam = ToTeam(t);
                teams.Add(curTeam);
                foreach (Guide g in t.Guides)
                {
                    LastnameFirst(g);
                    g.TeamId = curTeam.Id.ToString();
                    g.Id = ObjectId.GenerateNewId().ToString();
                    guides.Add(g);
                }
            }

            await teamLogic.Add(teams);
            await guide.Add(guides);
        }

        private List<ImportTeam> GetImportedTeams()
        {
            return JsonReader<ImportTeam>.ReadJson("./../Teams.json");
        }

        private Team ToTeam(ImportTeam t)
        {
            return new Team()
            {
                Name = t.Name,
                Id = ObjectId.GenerateNewId().ToString()
            };
        }

        private void LastnameFirst(Guide g)
        {
            string[] parts = g.Name.Split(" ");
            int numOfParts = parts.Length;
            if (numOfParts > 1)
            {
                string result = parts[numOfParts - 1];
                for (int i = 0; i < numOfParts - 1; i++)
                {
                    result += (" " + parts[i]);
                }
                g.Name = result;
            }
           
        }
    }
}
