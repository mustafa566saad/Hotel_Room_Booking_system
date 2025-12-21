namespace Hotel_Room_Booking_system.Interfaces
{
    public interface IReviewRepo : IBaseRepo<Review>
    {
        public Task AddReview(ReviewDTO dTO);
        public Task DeleteReview(int reviwID);
        public Task UpdateReview(int reviewID, ReviewDTO dTO);
        public Task<List<ReviewDTO>> GetRoomReview(int RoomID);
        public Task<List<ReviewDTO>> GetAllReviews();   

    }

}
