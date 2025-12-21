
namespace Hotel_Room_Booking_system.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly HotelContext _hotelContext;

        public BaseRepo(HotelContext hotelContext)
        {
            _hotelContext = hotelContext;
        }
        public async Task<bool> CreateAsync(T entity)
        {
           var result= await _hotelContext.Set<T>().AddAsync(entity);
           return result.State == EntityState.Added;
        }

        public bool Delete(T entity)
        {
             var result= _hotelContext.Set<T>().Remove(entity);
             return result.State == EntityState.Deleted;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> Createria) => await _hotelContext.Set<T>().AnyAsync(Createria);

        public async Task<List<T>> GetAllAsync() => await _hotelContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)=> await _hotelContext.Set<T>().FindAsync(id);

        public async Task<T> GetByMatchingAsync(Expression<Func<T, bool>> expression) => await _hotelContext.Set<T>().FirstOrDefaultAsync(expression);

        public async Task UpdateAsync(T entity) => _hotelContext.Set<T>().Update(entity);
    }
}
