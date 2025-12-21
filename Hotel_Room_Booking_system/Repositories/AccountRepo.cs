
using AutoMapper;

namespace Hotel_Room_Booking_system.Repositories
{
    public class AccountRepo:BaseRepo<ApplicationUser>,IAccountRepo
    {
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepo(HotelContext context,IMapper mapper, UserManager<ApplicationUser> userManager) : base(context)
        {
            this.mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CheckPassword(ApplicationUser user, string Password)
        {
            return await _userManager.CheckPasswordAsync(user, Password);
        }

        public async Task<(bool Succeeded,IEnumerable<string> Errors)> RegisterAsync(RegisterDTO dTO)
        {
            var user=mapper.Map<ApplicationUser>(dTO);
            var result = await _userManager.CreateAsync(user, dTO.Password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));
            await _userManager.AddToRoleAsync(user, "User");
            return (true, Enumerable.Empty<string>());
        }

        public async Task DeleteUser(string email)
        {
            var user = await GetByMatchingAsync(u => u.Email == email);
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.DeleteAsync(user);
        }



    }
}
