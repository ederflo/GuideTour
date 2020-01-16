using GuideTourData.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Services
{
    public interface IDocumentDbRepository
    {
        IMongoCollection<Team> Teams { get; }

        IMongoCollection<Guide> Guides { get; }

        IMongoCollection<Tour> Tours { get; }
    }
}
