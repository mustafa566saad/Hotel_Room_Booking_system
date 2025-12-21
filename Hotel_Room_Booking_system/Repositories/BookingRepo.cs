namespace Hotel_Room_Booking_system.Repositories
{
    public class BookingRepo:BaseRepo<Booking>, IBookingRepo
    {
        private readonly HotelContext _context;
        private readonly IUOWRepo _uOW;
        private readonly IMapper _mapper;

        public BookingRepo(HotelContext hotelContext, IUOWRepo uOW, IMapper mapper) : base(hotelContext)
        {
            _context = hotelContext;
            this._uOW = uOW;
            this._mapper = mapper;
        }

        public async Task<List<Booking>> GetBookingsByUserEmailAsync(string Email)
        {
            var user=await _uOW.AccountRepo.GetByMatchingAsync(e => e.Email == Email);
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.UserId == user.Id)
                .Include(b => b.Room)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetAllBookingAsync()=> await _context.Bookings.AsNoTracking().ToListAsync();
        

        public async Task RoomBoking(BookingDTO dTO)
        {
            var user = await _uOW.AccountRepo.GetByMatchingAsync(e => e.Email == dTO.UserEmail);
            var booking = _mapper.Map<Booking>(dTO);
            booking.UserId = user.Id;
            var roomList =new List<Room> {await _uOW.RoomRepo.GetByIdAsync(dTO.RoomId) };
            var newRoom = await _uOW.RoomRepo.CalculateSeasonPercantge(roomList, dTO.CheckIn, dTO.CheckOut);
            booking.TotalPrice = (decimal)newRoom.First().TotalPrice;
            await CreateAsync(booking);
            var room = await _uOW.RoomRepo.GetByIdAsync(dTO.RoomId);

            room.Status = RoomStatus.Unavailable.ToString();
        }

        public async Task<bool> DeclineBooking(int bookingID)
        {
            var book = await GetByIdAsync(bookingID);
            var room = await _uOW.RoomRepo.GetByIdAsync(book.RoomId);
            room.Status = RoomStatus.Available.ToString();
            return Delete(book);
        }

    }   

}

