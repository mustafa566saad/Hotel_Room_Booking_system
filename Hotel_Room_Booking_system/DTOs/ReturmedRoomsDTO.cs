namespace Hotel_Room_Booking_system.DTOs
{
    public class ReturmedRoomsDTO
    {
        public int RoomId { get; set; }

        [Range(1, 10)]
        public int Capacity { get; set; } = (int)CapacityRoom.Single;

        public double PricePerDay { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = RoomStatus.Available.ToString();

        public string Description { get; set; }

        public double TotalPrice { get; set; }

        public ICollection<string>? ImagesURLs { get; set; }
    }
}
