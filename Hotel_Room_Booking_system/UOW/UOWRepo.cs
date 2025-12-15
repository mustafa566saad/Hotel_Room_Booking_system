namespace Hotel_Room_Booking_system.UOW
{
    public class UOWRepo : IUOWRepo
    {
        private readonly HotelContext context;
        public IAccountRepo AccountRepo { get; private set; }
        public IRoomRepo RoomRepo { get; private set; }
        public IBookingRepo BookingRepo { get; private set; }

        public UOWRepo(HotelContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            AccountRepo = new AccountRepo(this.context, mapper, userManager);
            RoomRepo = new RoomRepo(this.context, mapper);
            BookingRepo = new BookingRepo(this.context);
        }
       
        public void Dispose()
        {
            context.Dispose();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
