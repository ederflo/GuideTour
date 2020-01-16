using GuideTourData.Models;
using GuideTourData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.DataAccess
{
    public class TeamDataAccess : IDataAccess<Team>
    {
        private readonly IDocumentDbRepository _ddb;

        public TeamDataAccess (IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }



        public static readonly Dictionary<string, Team> teams = new Dictionary<string, Team>
        {
            {
                "Team A",
                new Team()
                {
                    Name = "Team A"
                }
            },
            {
                "Team B",
                new Team()
                {
                    Name = "Team B"
                }
            },
            {
                "Team C",
                new Team()
                {
                    Name = "Team C"
                }
            },
            {
                "Team D",
                new Team()
                {
                    Name = "Team D"
                }
            },
            {
                "Team E",
                new Team()
                {
                    Name = "Team E"
                }
            },
            {
                "Team F",
                new Team()
                {
                    Name = "Team F"
                }
            },
            {
                "Team G",
                new Team()
                {
                    Name = "Team G"
                }
            }
            ,
            {
                "Team H",
                new Team()
                {
                    Name = "Team H"
                }
            }
            ,
            {
                "Team I",
                new Team()
                {
                    Name = "Team I"
                }
            }
        };

        public Task<Team> GetItemAsync(Expression<Func<Team, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetItemByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Team>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Team>> GetItemsAsync(Expression<Func<Team, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Team> CreateItemAsync(Team item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Team>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<Team> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<Team> UpdateItemAsync(Team item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
