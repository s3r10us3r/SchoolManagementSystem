using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class AssignmentRepo : BaseRepo<Assignment>, IAssignmentRepo
    {
        public AssignmentRepo(SmsDbContext db) : base(db)
        {
        }
    }
}
