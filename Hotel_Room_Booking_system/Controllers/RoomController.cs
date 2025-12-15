namespace Hotel_Room_Booking_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class RoomController : ControllerBase
    {
        private readonly IUOWRepo _uOW;

        public RoomController(IUOWRepo uOW)  
        {
            _uOW = uOW;
        }

        [HttpPost("AddRoom")]
        public async Task<IActionResult> AddRoom([FromBody] RoomDTO dTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(string.IsNullOrWhiteSpace(dTO.Description))
                return BadRequest("Description cannot be empty");

            await _uOW.RoomRepo.AddRoomAsync(dTO);
            await _uOW.SaveAsync();
            return Ok("Room Added Successfully");
        }

        [HttpGet("GetAllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var result= await _uOW.RoomRepo.GetAllRoomsAsync();
            return Ok(result);
        }

        [HttpGet("GetAvailableRooms")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] DateTime checkIn, [FromQuery] DateTime checkOut)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (checkIn >= checkOut)
                return BadRequest("Check-out date must be after check-in date.");
            var result = await _uOW.RoomRepo.GetAvailableRoomsAsync(checkIn, checkOut);
            return Ok(result);
        }

        [HttpGet("GetRoomByPrice")]
        public async Task<IActionResult> GetRoomsByPrice([FromQuery] double minPrice, [FromQuery] double maxPrice)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (minPrice < 0 || maxPrice < 0)
                return BadRequest("Prices must be non-negative.");
            if (minPrice > maxPrice)
                return BadRequest("Minimum price cannot be greater than maximum price.");
            var result = await _uOW.RoomRepo.GetRoomsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(result);
        }

        [HttpGet("GetRoomsByCapacity")]
        public async Task<IActionResult> GetRoomsByCapacity([FromQuery] int capacity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (capacity <= 0)
                return BadRequest("Capacity must be a positive integer.");
            if (capacity > 5)
                return BadRequest("Capacity exceeds maximum limit of 5.");

            var result = await _uOW.RoomRepo.GetRoomsByCapacityAsync(capacity);
            return Ok(result);
        }

        [HttpGet("GetRoomDetails")]
        public async Task<IActionResult> GetRoomDetails([FromQuery] int roomId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _uOW.RoomRepo.GetRoomDetailsAsync(roomId);
            return Ok(result);
        }



    }
}
