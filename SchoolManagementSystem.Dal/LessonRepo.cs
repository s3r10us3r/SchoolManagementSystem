using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class LessonRepo : BaseRepo<Lesson>, ILessonRepo
    {
        public LessonRepo(DbContext db) : base(db)
        {
        }
    }
}
