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
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            var result = await teacherDataAccess.GetAllItemsAsync();
            return result.ToList();
        }

        public async Task<Teacher> Get(string teacherId)
        {
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            return await teacherDataAccess.GetItemByIdAsync(teacherId);
        }

        public async Task<Teacher> GetByPinCode(int pinCode)
        {
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            return await teacherDataAccess.GetItemAsync(x => x.PinCode == pinCode);
        }

        public async Task<Teacher> Add(Teacher teacher)
        {
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            return await teacherDataAccess.CreateItemAsync(teacher);
        }

        public async Task<List<Teacher>> Add(List<Teacher> teachers)
        {
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            return await teacherDataAccess.CreateItemsAsync(teachers);
        }

        public async Task<Teacher> Update(Teacher teacher)
        {
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            return await teacherDataAccess.UpdateItemAsync(teacher);
        }

        public async Task<bool> Delete(string teacherId)
        {
            TeacherDataAccess teacherDataAccess = new TeacherDataAccess(_ddb);
            return await teacherDataAccess.DeleteItemAsync(teacherId);
        }

        public async Task<string> CheckLastAction(string teacherId)
        {
            bool permissions = false;
            Teacher t = await Get(teacherId);
            permissions = CheckLastAction(t);
            if (permissions)
            {
                t.LastAction = DateTime.Now;
                await Update(t);
            }
            else
                teacherId = null;

            return teacherId;
        }

        public async Task<string> CheckPinCode(int pinCode)
        {
            string teacherId = null;
            Teacher t = await GetByPinCode(pinCode);
            if (t != null)
            {
                t.LastAction = DateTime.Now;
                await Update(t);
                teacherId = t.Id;
            }
                
            return teacherId;
        }

        private static bool CheckLastAction(Teacher t)
        {
            bool result = false;
            if (t != null && t.LastAction != null)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan timeSpan = currentTime - (DateTime) t.LastAction;
                result = timeSpan.TotalSeconds <= 120;
            }
            return result;
        }
    }
}
