using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Room_Booking_system.Models
{
    public class SeasonalPrice
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Range(0, 1)]
        public decimal IncreasePercentage { get; set; } = .25m;

        public bool IsActive (DateTime checkIn, DateTime checkOut) => checkIn <= EndDate && checkOut >= StartDate;
    }
}
