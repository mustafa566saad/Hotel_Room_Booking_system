namespace Hotel_Room_Booking_system.Interfaces
{
    public interface IBookingRepo:IBaseRepo<Booking>
    {
        public Task<List<Booking>> GetBookingsByUserEmailAsync(string Email);
        public Task<List<Booking>> GetAllBookingAsync();
    }
}
