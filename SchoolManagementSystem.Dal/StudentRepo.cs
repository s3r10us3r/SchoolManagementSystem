using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class StudentRepo : BaseRepo<Student>, IStudentRepo
    {
        public StudentRepo(SmsDbContext db) : base(db)
        {
        }
    }
}
