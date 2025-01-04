using SchoolManagementSystem.Models;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Dal.Interfaces
{
    public interface IRepo<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
