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
    public class TeacherLogic
    {
        private readonly IDocumentDbRepository _ddb;

        public TeacherLogic(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task<List<Teacher>> Get()
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess(_ddb);
            var result = await tourDataAccess.GetAllItemsAsync();
            return result.ToList();
        }

        public async Task<Teacher> Get(string teamname)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess(_ddb);
            return await tourDataAccess.GetItemByIdAsync(teamname);
        }

        public async Task<Teacher> Add(Teacher teacher)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess(_ddb);
            return await tourDataAccess.CreateItemAsync(teacher);
        }

        public async Task<Teacher> Update(Teacher teacher)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess(_ddb);
            return await tourDataAccess.UpdateItemAsync(teacher);
        }

        public async Task<bool> Delete(string teacherId)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess(_ddb);
            return await tourDataAccess.DeleteItemAsync(teacherId);
        }
    }
}
