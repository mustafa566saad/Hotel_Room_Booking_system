
namespace Hotel_Room_Booking_system.UOW
{
    public interface IUOWRepo : IDisposable
    {
        IAccountRepo AccountRepo { get; }
        IRoomRepo RoomRepo { get; }
        IBookingRepo BookingRepo { get; }
        IReviewRepo ReviewRepo { get; }

        Task<int> SaveAsync();
    }
}
