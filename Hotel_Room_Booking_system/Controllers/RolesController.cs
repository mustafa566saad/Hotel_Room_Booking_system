using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hotel_Room_Booking_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUOWRepo _uow;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager,IUOWRepo uOW,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _uow = uOW;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            var roleExists = _roleManager.RoleExistsAsync(roleName).Result;
            if (!roleExists)
            {
                var result = _roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                if (result.Succeeded)
                {
                    await _uow.SaveAsync();
                    return Ok($"Role '{roleName}' created successfully.");
                }

                return BadRequest("Error creating role.");
            }
            return BadRequest("Role already exists.");
        }
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = _roleManager.FindByNameAsync(roleName).Result;
            if (role != null)
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    await _uow.SaveAsync();
                    return Ok($"Role '{roleName}' deleted successfully.");
                }
                return BadRequest("Error deleting role.");
            }
            return NotFound("Role not found.");
        }

        [HttpPost("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole(string UserEmail, string NewRole)
        {
            //Check if role exists
            var CheckRole = await _roleManager.RoleExistsAsync(NewRole);
            if(!CheckRole)
                return NotFound("Role not found.");

            //Check if user exists
            var UserCheck =await _uow.AccountRepo.ExistsAsync(e=>e.Email==UserEmail);
            if(!UserCheck)
                return NotFound("User not found.");

            //Check if user already in role
            var user = await _uow.AccountRepo.GetByMatchingAsync(e => e.Email == UserEmail);
            var roleExists = await userManager.GetRolesAsync(user);
            if (roleExists.Contains(NewRole))
                return BadRequest("User already in role.");

            
            var result =await userManager.AddToRoleAsync(user, NewRole);
            if (result.Succeeded)
            {
                await _uow.SaveAsync();
                return Ok("User role changed successfully.");
            }

            return BadRequest("Error changing user role.");
        }

    }
}
