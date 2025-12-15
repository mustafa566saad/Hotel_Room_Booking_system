
namespace Hotel_Room_Booking_system.Interfaces
{
    public interface ILoginServices
    {
        Task<AuthUserDTO> LoginAsync(LoginDTO DTO);
    }
}
