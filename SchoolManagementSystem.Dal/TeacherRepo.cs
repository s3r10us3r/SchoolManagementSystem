using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class TeacherRepo : BaseRepo<Teacher>, ITeacherRepo
    {
        public TeacherRepo(DbContext db) : base(db)
        {
        }
    }
}
