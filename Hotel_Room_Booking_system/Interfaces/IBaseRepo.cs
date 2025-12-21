using System.Linq.Expressions;

namespace Hotel_Room_Booking_system.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<bool> CreateAsync(T entity);
        public Task UpdateAsync(T entity);
        public bool Delete(T entity);
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> Createria);
        public  Task<T> GetByMatchingAsync(Expression<Func<T, bool>> Createria);
    }
}
