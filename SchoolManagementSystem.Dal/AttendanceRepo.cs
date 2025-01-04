using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class AttendanceRepo : BaseRepo<Attendance>, IAttendanceRepo
    {
        public AttendanceRepo(DbContext db) : base(db)
        {
        }
    }
}
