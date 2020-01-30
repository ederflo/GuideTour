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
        private readonly IMongoDatabase _ddb;

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
            var optionsUnique = new CreateIndexOptions() { Unique = true };
            var fieldTeamName = new StringFieldDefinition<Team>("Name");
            var fieldTeacherPinCode = new StringFieldDefinition<Teacher>("PinCode");
            var fieldGuideEmail = new StringFieldDefinition<Guide>("Email");
            var fieldTourIfGuideAppId = new StringFieldDefinition<Tour>("IfGuideAppId");
            var indexDefinitionTeamName = new IndexKeysDefinitionBuilder<Team>().Ascending(fieldTeamName);
            var indexDefinitionTeacherPinCode = new IndexKeysDefinitionBuilder<Teacher>().Ascending(fieldTeacherPinCode);
            var indexDefinitionGuideEmail = new IndexKeysDefinitionBuilder<Guide>().Ascending(fieldGuideEmail);
            var indexDefinitionTourIfGuideAppId = new IndexKeysDefinitionBuilder<Tour>().Ascending(fieldTourIfGuideAppId);

            var fieldTourGuideId = new StringFieldDefinition<Tour>("GuideId");
            var fieldTourStartedTour = new StringFieldDefinition<Tour>("StartedTour");
            var fieldTourEndedTour = new StringFieldDefinition<Tour>("EndedTour");
            var indexDefinitionTourGuideId = new IndexKeysDefinitionBuilder<Tour>().Ascending(fieldTourGuideId);
            var indexDefinitionTourStartedTour = new IndexKeysDefinitionBuilder<Tour>().Ascending(fieldTourStartedTour);
            var indexDefinitionTourEnded = new IndexKeysDefinitionBuilder<Tour>().Ascending(fieldTourEndedTour);
            var combinedIndexDefinitionNotStarted = new IndexKeysDefinitionBuilder<Tour>()
                .Combine(indexDefinitionTourGuideId, indexDefinitionTourStartedTour);
            var combinedIndexDefinitionTourEnded = new IndexKeysDefinitionBuilder<Tour>()
                .Combine(indexDefinitionTourGuideId, indexDefinitionTourEnded);

            _ddb.GetCollection<Team>("Teams").Indexes.CreateOne(indexDefinitionTeamName, optionsUnique);
            _ddb.GetCollection<Teacher>("Teachers").Indexes.CreateOne(indexDefinitionTeacherPinCode, optionsUnique);
            //_ddb.GetCollection<Tour>("Tours").Indexes.CreateOne(indexDefinitionTourIfGuideAppId, optionsUnique);
            _ddb.GetCollection<Guide>("Guides").Indexes.CreateOne(indexDefinitionGuideEmail, optionsUnique);
            _ddb.GetCollection<Tour>("Tours").Indexes.CreateOne(combinedIndexDefinitionNotStarted, optionsUnique);
            _ddb.GetCollection<Tour>("Tours").Indexes.CreateOne(combinedIndexDefinitionTourEnded, optionsUnique);
        }

        public IMongoCollection<Team> Teams => _ddb.GetCollection<Team>("Teams");
        public IMongoCollection<Guide> Guides => _ddb.GetCollection<Guide>("Guides");
        public IMongoCollection<Tour> Tours => _ddb.GetCollection<Tour>("Tours");
        public IMongoCollection<Teacher> Teachers => _ddb.GetCollection<Teacher>("Teachers");
    }
}
