using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Booking>> GetBookingsByUserEmail(string email)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(string.IsNullOrWhiteSpace(email))
                return BadRequest("Email cannot be null or empty.");

            var checkEmail = await _UOW.AccountRepo.ExistsByEmailAsync(e=>e.Email==email);
            if(!checkEmail)
                return BadRequest("No user found with the provided email.");
            var booking = await _UOW.BookingRepo.GetBookingsByUserEmailAsync(email);
            return Ok(booking);
        }

        [HttpGet("GetAllBookings")]
        public async Task<ActionResult<Booking>> GetAllBookings()
        {
            var bookings = await _UOW.BookingRepo.GetAllBookingAsync();
            return Ok(bookings);
        }


    }
}
