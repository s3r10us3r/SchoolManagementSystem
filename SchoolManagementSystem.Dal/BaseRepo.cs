using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Dal.Interfaces;
using SchoolManagementSystem.Models;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Dal
{
    public abstract class BaseRepo<T> : IRepo<T> where T : Entity
    {
        protected DbContext Db { get; private init; }
        protected DbSet<T> Table { get; private init; }

        public BaseRepo(DbContext db)
        {
            Db = db;
            Table = db.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            Table.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            Table.Remove(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Table.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
