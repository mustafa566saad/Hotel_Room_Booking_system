
using Microsoft.AspNetCore.Authorization;

namespace Hotel_Room_Booking_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
        private readonly IUOWRepo _uow;

        public AccountController(ILoginServices loginServices, IUOWRepo uOW)
        {
            _loginServices = loginServices;
            _uow = uOW;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Email Existence Check
            var EmailCheck = await _uow.AccountRepo.ExistsByEmailAsync(e => e.Email == DTO.Email);
            if (!EmailCheck)
                return NotFound("User or email not correct");

            var user = await _uow.AccountRepo.GetByEmailAsync(e => e.Email == DTO.Email);

            //Password Check
            var PassCheck = await _uow.AccountRepo.CheckPassword(user, DTO.Password);
            if (!PassCheck)
                return NotFound("User or email not correct");


            var result = await _loginServices.LoginAsync(DTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Email Existence Check
            var EmailCheck = await _uow.AccountRepo.ExistsByEmailAsync(e => e.Email == DTO.Email);
            if (EmailCheck)
                return BadRequest("Email already exists");

            var result = await _uow.AccountRepo.RegisterAsync(DTO);
            if (!result.Succeeded)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            var token = await _loginServices.LoginAsync(new LoginDTO { Email = DTO.Email, Password = DTO.Password });
            await _uow.SaveAsync();
            return Ok(token);
        }

        [HttpGet("GetAllUsers")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _uow.AccountRepo.GetAllAsync();
            return Ok(users);
        }

        [HttpDelete("DeleteUser")]
        // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUser([FromQuery] string email)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var userExists = await _uow.AccountRepo.ExistsByEmailAsync(u => u.Email == email);
            if (!userExists)
                return NotFound("User not found");
            await _uow.AccountRepo.DeleteUser(email);
            return Ok("User Deleted");
        }
    }
}
