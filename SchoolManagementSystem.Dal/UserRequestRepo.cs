﻿using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Dal
{
    public class UserRequestRepo : BaseRepo<UserRequest>, IUserRequestRepo
    {
        public UserRequestRepo(SmsDbContext db) : base(db)
        {
        }
    }
}
