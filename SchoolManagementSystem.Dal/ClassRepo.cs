using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class ClassRepo : BaseRepo<Class>, IClassRepo
    {
        public ClassRepo(DbContext db) : base(db)
        {
        }
    }
}
