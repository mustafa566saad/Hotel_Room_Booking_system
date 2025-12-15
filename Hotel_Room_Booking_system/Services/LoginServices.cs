namespace Hotel_Room_Booking_system.Services
{
    public class LoginServices: ILoginServices
    {
        private readonly IUOWRepo _UOWRepo;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginServices(IUOWRepo UOWRepo, IConfiguration config,UserManager<ApplicationUser> userManager)
        {
            _UOWRepo = UOWRepo;
            _config = config;
            _userManager = userManager;
        }

        public async Task<AuthUserDTO> LoginAsync(LoginDTO DTO)
        {
            var user = await _UOWRepo.AccountRepo.GetByEmailAsync(u => u.Email == DTO.Email);
            var token = GetJWTTokenAsync(user);
            AuthUserDTO authUser = new AuthUserDTO()
            {
                Email = user.Email,
                Token = await token,
                id = user.Id
            };
            return authUser;
        }


        // Token Generation Method
        private async Task<string> GetJWTTokenAsync(ApplicationUser user)
        {

            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.Name, user.Email));
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var userRoles = await _userManager.GetRolesAsync(user);
            Claims.AddRange(from role in userRoles
                            select new Claim(ClaimTypes.Role, role));

            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var signingCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(60),
                claims: Claims,
                signingCredentials: signingCredentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
