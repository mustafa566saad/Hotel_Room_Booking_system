namespace Hotel_Room_Booking_system.Repositories
{
    public class BookingRepo:BaseRepo<Booking>, IBookingRepo
    {
        private readonly HotelContext _context;
        public BookingRepo(HotelContext hotelContext) : base(hotelContext)
        {
            _context = hotelContext;
        }

        public async Task<List<Booking>> GetBookingsByUserEmailAsync(string Email)
        {
            var user=await _context.Users.FirstOrDefaultAsync(e => e.Email == Email);
            return await _context.Bookings
                .Where(b => b.UserId == user.Id)
                .Include(b => b.Room)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetAllBookingAsync()
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .ToListAsync();
        }

        public async Task BookRoom()

    }   

}

