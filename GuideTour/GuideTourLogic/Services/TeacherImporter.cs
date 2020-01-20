using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourLogic.Services
{
    public class TeacherImporter
    {
        private readonly IDocumentDbRepository _ddb;

        public TeacherImporter(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public async Task ImportTeachers()
        {
            TeacherLogic teacherLogic = new TeacherLogic(_ddb);
            List<Teacher> importedTeachers = GetTeachers();

            if (importedTeachers != null)
                await teacherLogic.Add(importedTeachers);
        }

        private static List<Teacher> GetTeachers()
        {
            return JsonReader<Teacher>.ReadJson("./../Teachers.json");
        }
    }
}
