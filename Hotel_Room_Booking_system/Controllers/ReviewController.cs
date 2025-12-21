
namespace Hotel_Room_Booking_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUOWRepo _uowRepo;
        public ReviewController(IUOWRepo uowRepo)
        {
            _uowRepo = uowRepo;
        }

        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _uowRepo.ReviewRepo.GetAllReviews();
            return Ok(reviews);
        }
        [HttpGet("GetRoomReview")]
        public async Task<IActionResult> GetRoomReview(int RoomID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkID = await _uowRepo.RoomRepo.ExistsAsync(r => r.RoomId == RoomID);
            if (!checkID)
                return BadRequest("Invalid Room ID");
            var reviews = await _uowRepo.ReviewRepo.GetRoomReview(RoomID);
            return Ok(reviews);
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO dTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkRoomID = await _uowRepo.RoomRepo.ExistsAsync(r => r.RoomId == dTO.RoomId);
            if (!checkRoomID)
                return BadRequest("Invalid Room ID");
            var checkUserID = await _uowRepo.AccountRepo.ExistsAsync(u => u.Id == dTO.UserId);
            if (!checkUserID)
                return BadRequest("Invalid User ID");
            await _uowRepo.ReviewRepo.AddReview(dTO);
            await _uowRepo.SaveAsync();
            return Ok("Review Added Successfully");
        }

        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview(int reviewID, [FromBody] ReviewDTO dTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkReviewID = await _uowRepo.ReviewRepo.ExistsAsync(r => r.ReviewId == reviewID);
            if (!checkReviewID)
                return BadRequest("Invalid Review ID");
            var checkRoomID = await _uowRepo.RoomRepo.ExistsAsync(r => r.RoomId == dTO.RoomId);
            if (!checkRoomID)
                return BadRequest("Invalid Room ID");
            await _uowRepo.ReviewRepo.UpdateReview(reviewID, dTO);
            await _uowRepo.SaveAsync();
            return Ok("Review Updated Successfully");
        }

        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> DeleteReview(int reviewID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var checkReviewID = await _uowRepo.ReviewRepo.ExistsAsync(r => r.ReviewId == reviewID);
            if (!checkReviewID)
                return BadRequest("Invalid Review ID");
            await _uowRepo.ReviewRepo.DeleteReview(reviewID);
            await _uowRepo.SaveAsync();
            return Ok("Review Deleted Successfully");
        }

    }
}
