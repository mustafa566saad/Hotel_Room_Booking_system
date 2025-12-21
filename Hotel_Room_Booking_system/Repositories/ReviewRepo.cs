namespace Hotel_Room_Booking_system.Repositories
{
    public class ReviewRepo:BaseRepo<Review>, IReviewRepo
    {
        private readonly HotelContext _context;
        private readonly IMapper _mapper;
        private readonly IUOWRepo _uOW;

        public ReviewRepo(HotelContext context,IMapper mapper,IUOWRepo uOW) : base(context)
        {
            _mapper = mapper;
            _uOW = uOW;
            _context = context;
        }

        public async Task AddReview(ReviewDTO dTO)
        {
            var review = _mapper.Map<Review>(dTO);
            var room=await _uOW.RoomRepo.GetByIdAsync(dTO.RoomId);
            room.Reviews.Add(review);
            await CreateAsync(review);
        }
        public async Task DeleteReview(int reviwID)
        {
            var review =await GetByIdAsync(reviwID);
            var room = await _uOW.RoomRepo.GetByIdAsync(review.RoomId);
            room.Reviews.Remove(review);
            Delete(review);
        }
        public async Task UpdateReview(int reviewID, ReviewDTO dTO)
        {
            var existingReview = await GetByIdAsync(reviewID);
            if (existingReview != null)
            {
                _mapper.Map(dTO, existingReview);
                var room = await _uOW.RoomRepo.GetByIdAsync(dTO.RoomId);
                var newReview = room.Reviews.FirstOrDefault(e => e.ReviewId == reviewID);
                newReview.Rating=dTO.Rating;
                newReview.Comment=dTO.Comment;
                await _context.Reviews.ExecuteUpdateAsync(e => e
                                                                    .SetProperty(r => r.Rating, r => newReview.Rating)
                                                                    .SetProperty(r => r.Comment, r => newReview.Comment));
            }
        }

        public async Task<List<ReviewDTO>> GetAllReviews()
        {
            var reviews = await GetAllAsync();
            return _mapper.Map<List<ReviewDTO>>(reviews);
        }
        public async Task<List<ReviewDTO>> GetRoomReview(int RoomID)
        {
            var room = await _uOW.RoomRepo.GetByIdAsync(RoomID);
            return _mapper.Map<List<ReviewDTO>>(room.Reviews);
        }

    }
}
