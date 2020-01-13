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
                "Team A",
                new Team()
                {
                    Name = "Team A",
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
                "Team B",
                new Team()
                {
                    Name = "Team B",
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
                "Team C",
                new Team()
                {
                    Name = "Team C",
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
            },
            {
                "Team D",
                new Team()
                {
                    Name = "Team D",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Mister X",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Mister Y",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Mister Z",
                            Email = "reschn@edu.htl-villach.at"
                        }
                    }
                }
            },
            {
                "Team E",
                new Team()
                {
                    Name = "Team E",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Lelekt",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Magnesium Manuel",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Karsten Stahl",
                            Email = "reschn@edu.htl-villach.at"
                        }
                    }
                }
            },
            {
                "Team F",
                new Team()
                {
                    Name = "Team F",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Fette Beate",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Klana Schlumpf",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Kreta Kliemann",
                            Email = "reschn@edu.htl-villach.at"
                        }
                    }
                }
            },
            {
                "Team G",
                new Team()
                {
                    Name = "Team G",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Hagen Kreuz",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Bauchspeichel",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Manfred Bauer",
                            Email = "reschn@edu.htl-villach.at"
                        }
                    }
                }
            }
            ,
            {
                "Team H",
                new Team()
                {
                    Name = "Team H",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Dick Tator",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Holda Daraus",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Meta Bolien",
                            Email = "reschn@edu.htl-villach.at"
                        }
                    }
                }
            }
            ,
            {
                "Team I",
                new Team()
                {
                    Name = "Team I",
                    Guides = new List<Guide>
                    {
                        new Guide()
                        {
                            Name = "Einstein",
                            Email = "koderf@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Zweistein",
                            Email = "kreuzerl@edu.htl-villach.at"
                        },
                        new Guide()
                        {
                            Name = "Dreistein",
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
