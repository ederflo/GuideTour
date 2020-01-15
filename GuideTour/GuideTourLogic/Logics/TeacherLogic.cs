using GuideTourData.DataAccess;
using GuideTourData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourLogic.Logics
{
    public class TeacherLogic
    {
        public List<Teacher> Get()
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess();
            return tourDataAccess.Get();
        }

        public Teacher Get(string teamname)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess();
            return tourDataAccess.Get(teamname);
        }

        public Teacher Add(Teacher teacher)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess();
            return tourDataAccess.Add(teacher);
        }

        public Teacher Update(Teacher teacher)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess();
            return tourDataAccess.Update(teacher);
        }

        public bool Delete(string teacherId)
        {
            TeacherDataAccess tourDataAccess = new TeacherDataAccess();
            return tourDataAccess.Delete(teacherId);
        }
    }
}
