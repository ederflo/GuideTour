using GuideTourData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuideTourData.DataAccess
{
    public class TeacherDataAccess
    {
        Dictionary<string, Teacher> teachers = new Dictionary<string, Teacher>
        {
            {
                "edf",
                new Teacher()
                {
                    Id = "edf",
                    PinCode = 112212
                }
            }
        };

        public List<Teacher> Get()
        {
            return teachers.Values.ToList();
        }

        public Teacher Get(string teacherId)
        {
            return teachers[teacherId];
        }

        public Teacher Add(Teacher teacher)
        {
            if (teacher == null)
                return null;

            if (string.IsNullOrWhiteSpace(teacher.Id))
            {
                return null;
            }

            teachers.Add(teacher.Id, teacher);
            return teacher;
        }


        public Teacher Update(Teacher teacher)
        {
            if (teacher == null || string.IsNullOrWhiteSpace(teacher.Id))
                return null;

            Teacher teacherToUpdate = teachers[teacher.Id];

            if (teacherToUpdate != null)
                teacherToUpdate = teacher;

            return teacherToUpdate;
        }

        public bool Delete(string teacherId)
        {
            bool succeeded = false;

            if (string.IsNullOrWhiteSpace(teacherId))
                return succeeded;

            if (teachers.ContainsKey(teacherId))
                succeeded = teachers.Remove(teacherId);

            return succeeded;
        }
    }
}
