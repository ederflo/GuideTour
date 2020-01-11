using GuideTourData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.DataAccess
{
    public class TeamDataAccess
    {
        private static readonly Dictionary<string, Team> teams = new Dictionary<string, Team>
        {
            {
                "Bestes Team",
                new Team()
                {
                    Name = "Bestes Team",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Florian Eder",
                            Email = "ederf1@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Daniel Tschlatscher",
                            Email = "dtschlat@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Dominic Gradenegger",
                            Email = "gradened@edu.htl-villach.at"
                        }
                    }
                }
            },
            {
                "Team Villach",
                new Team()
                {
                    Name = "Team Villach",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Christian Pringruber",
                            Email = "pirngc@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Maximilian Gutschier",
                            Email = "gutschm@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Lukas Sterbenz",
                            Email = "sterbenzl@edu.htl-villach.at"
                        }
                    }
                }
            },
            {
                "Meiste Führungen",
                new Team()
                {
                    Name = "Meiste Führungen",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Fabian Koder",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Lukas Kreuzer",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Noah Resch",
                            Email = "reschn@edu.htl-villach.at"
                        }
                    }
                }
            }
        };


        public List<Team> Get()
        {
            return teams.Values.ToList();
        }

        public Team Get(string teamName)
        {
            return teams[teamName];
        }

        public Team Add(Team team)
        {
            if (team == null)
                return null;

            if (string.IsNullOrWhiteSpace(team.Name))
            {
                return null;
            }

            teams.Add(team.Name, team);
            return team;
        }


        public Team Update(Team team)
        {
            if (team == null || string.IsNullOrWhiteSpace(team.Name))
                return null;

            Team teamToUpdate = teams[team.Name];

            if (teamToUpdate != null)
                teamToUpdate = team;

            return teamToUpdate;
        }

        public bool Delete(string teamname)
        {
            bool succeeded = false;

            if (string.IsNullOrWhiteSpace(teamname))
                return succeeded;

            if (teams.ContainsKey(teamname))
                succeeded = teams.Remove(teamname);

            return succeeded;
        }
    }
}
