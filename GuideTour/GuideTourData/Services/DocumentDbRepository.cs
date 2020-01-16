using GuideTourData.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Services
{
    public class DocumentDbRepository : IDocumentDbRepository
    {
        IMongoDatabase _ddb;

        [Obsolete]
        public DocumentDbRepository(IOptions<DbProperties> settings)
        {
            MongoClient client = new MongoClient(settings.Value.ConnectionString);
            _ddb = client.GetDatabase(settings.Value.DatabaseName);

            CreateIndex();
        }

        [Obsolete]
        private void CreateIndex()
        {
            var options = new CreateIndexOptions() { Unique = true };
            var field = new StringFieldDefinition<Team>("Name");
            var indexDefinition = new IndexKeysDefinitionBuilder<Team>().Ascending(field);
            _ddb.GetCollection<Team>("Teams").Indexes.CreateOne(indexDefinition, options);
        }

        public IMongoCollection<Team> Teams => _ddb.GetCollection<Team>("Teams");
        public IMongoCollection<Guide> Guides => _ddb.GetCollection<Guide>("Guides");
        public IMongoCollection<Tour> Tours => _ddb.GetCollection<Tour>("Tours");
        public IMongoCollection<Teacher> Teachers => _ddb.GetCollection<Teacher>("Teacher");
    }
}
