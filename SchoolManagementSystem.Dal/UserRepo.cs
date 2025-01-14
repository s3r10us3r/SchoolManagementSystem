using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(SmsDbContext db) : base(db)
        {
        }
    }
}
