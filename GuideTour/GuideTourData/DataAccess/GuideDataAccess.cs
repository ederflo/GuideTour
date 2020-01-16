using GuideTourData.Models;
using GuideTourData.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.DataAccess
{
    public class GuideDataAccess : IDataAccess<Guide>
    {
        private readonly IDocumentDbRepository _ddb;

        public GuideDataAccess(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public static List<Guide> guides = new List<Guide>
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
            },
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
            },
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
            },
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
            },
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
            },
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
            },
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
            },
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
            },
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
        };

        public async Task<Guide> GetItemAsync(Expression<Func<Guide, bool>> predicate)
        {
            var result = await _ddb.Guides.FindAsync(predicate);
            List<Guide> guides = await result.ToListAsync();
            if (guides.Count > 1)
                return null;
            return guides.FirstOrDefault();
        }

        public async Task<Guide> GetItemByIdAsync(string id)
        {
            var result = await _ddb.Guides.FindAsync(Builders<Guide>.Filter.Eq(x => x.Id, id));
            List<Guide> guides = await result.ToListAsync();
            if (guides.Count > 1)
                return null;
            return guides.FirstOrDefault();
        }

        public Task<IEnumerable<Guide>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guide>> GetItemsAsync(Expression<Func<Guide, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Guide> CreateSingleQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Guide>> CreateQueryAsync(string sqlExpression)
        {
            throw new NotImplementedException();
        }

        public Task<Guide> CreateItemAsync(Guide item)
        {
            throw new NotImplementedException();
        }

        public async Task<Guide> UpdateItemAsync(Guide item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
