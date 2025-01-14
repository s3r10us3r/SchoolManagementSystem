using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class OccuranceRepo : BaseRepo<Occurence>, IOccuranceRepo
    {
        public OccuranceRepo(SmsDbContext db) : base(db)
        {
        }
    }
}
