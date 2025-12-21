namespace Hotel_Room_Booking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IUOWRepo _UOW;
        public BookingController(IUOWRepo UOW)
        {
            _UOW = UOW;
        }

        [HttpGet("GetBookingsByUserEmail")]
        public async Task<IActionResult> GetBookingsByUserEmail(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email cannot be null or empty.");

            var checkEmail = await _UOW.AccountRepo.ExistsAsync(e => e.Email == email);
            if (!checkEmail)
                return BadRequest("No user found with the provided email.");
            var booking = await _UOW.BookingRepo.GetBookingsByUserEmailAsync(email);
            return Ok(booking);
        }

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _UOW.BookingRepo.GetAllBookingAsync();
            return Ok(bookings);
        }

        [HttpPost("BookingRoom")]
        public async Task<IActionResult> BookRoom([FromBody] BookingDTO dTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkEmail = await _UOW.AccountRepo.ExistsAsync(e => e.Email == dTO.UserEmail);
            if (!checkEmail)
                return BadRequest("No user found with the provided email.");
            if (string.IsNullOrWhiteSpace(dTO.UserEmail))
                return BadRequest("User email cannot be null or empty.");
            if (dTO.CheckIn > dTO.CheckOut)
                return BadRequest("Check-out date must be after check-in date.");
            await _UOW.BookingRepo.RoomBoking(dTO);
            await _UOW.SaveAsync();
            return Ok("Room booked successfully.");


        }

        [HttpPost("DeleteBooking")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var CheckId = await _UOW.BookingRepo.ExistsAsync(e=>e.BookingId == id);
            if (!CheckId)
                return BadRequest("Booking not found");
            var state = await _UOW.BookingRepo.DeclineBooking(id);
            return Ok("Booking deleted");

        }
    }
}
