namespace Hotel_Room_Booking_system.UOW
{
    public class UOWRepo : IUOWRepo
    {
        private readonly  HotelContext context;
        public IAccountRepo AccountRepo { get; private set; }
        public IRoomRepo RoomRepo { get; private set; }
        public IBookingRepo BookingRepo { get; private set; }
        public IReviewRepo ReviewRepo { get; private set; }

        public UOWRepo(HotelContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            context = context;
            AccountRepo = new AccountRepo(context, mapper, userManager);
            RoomRepo = new RoomRepo(context, mapper);
            BookingRepo = new BookingRepo(context, this, mapper);
            ReviewRepo = new ReviewRepo(context, mapper,this);
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
