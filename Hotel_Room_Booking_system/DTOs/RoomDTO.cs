namespace Hotel_Room_Booking_system.DTOs
{
    public class RoomDTO
    {
        public string Description { get; set; }
        public int Capacity { get; set; } = (int)CapacityRoom.Single;
        public decimal BasePrice { get; set; }
    }
}
