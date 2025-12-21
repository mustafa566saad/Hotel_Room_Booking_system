namespace Hotel_Room_Booking_system.Interfaces
{
    public interface IRoomRepo: IBaseRepo<Room>
    {
        public Task<List<ReturmedRoomsDTO>> GetAvailableRoomsAsync(DateTime checkIn, DateTime checkOut);
        public Task<List<ReturmedRoomsDTO>> GetAllRoomsAsync();
        public Task<Room> GetRoomDetailsAsync(int roomId);
        public Task<List<ReturmedRoomsDTO>> GetRoomsByPriceRangeAsync(double minPrice, double maxPrice);
        public Task<List<ReturmedRoomsDTO>> GetRoomsByCapacityAsync(int capacity);
        public Task AddRoomAsync(RoomDTO dTO);
        public Task<List<ReturmedRoomsDTO>> CalculateSeasonPercantge(List<Room> room, DateTime checkIn, DateTime checkOut);

    }
}
