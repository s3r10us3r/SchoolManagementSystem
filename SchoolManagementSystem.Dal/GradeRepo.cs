using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class GradeRepo : BaseRepo<Grade>, IGradeRepo
    {
        public GradeRepo(DbContext db) : base(db)
        {
        }
    }
}
