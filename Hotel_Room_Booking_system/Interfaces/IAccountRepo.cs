namespace Hotel_Room_Booking_system.Interfaces
{
    public interface IAccountRepo: IBaseRepo<ApplicationUser>
    {
        public Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterDTO dTO);
        public Task<bool> CheckPassword(ApplicationUser user, string Password);
        public Task DeleteUser(string email);


    }
}
