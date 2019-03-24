using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MP.Data;
using MP.Models.User;

namespace MP.Controllers {
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/{version:apiVersion}/users")]
    public class UsersController : ControllerBase {
        #region Private Members

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private AppDbContext _context;

        #endregion PrivateMembers
        
        #region Constructor

        public UsersController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context) {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        #endregion Constructor
        
        // GET: /api/1.0/users/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser([FromRoute] string id) {
            var userFromRepo = await _userManager.FindByIdAsync(id);
            if (userFromRepo == null) {
                return NotFound();
            }

            var claims = await _userManager.GetClaimsAsync(userFromRepo);

            var userToReturn = Mapper.Map<UserDto>(userFromRepo);
            userToReturn.UserClaims = claims.Select(c => c.Value).ToList();
            return Ok(userToReturn);
        }
        
        
        // POST: /api/1.0/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto dto) {
            if (dto == null) {
                return BadRequest();
            }

            if (!ModelState.IsValid) {
                return BadRequest(); // TODO: change to UnprocessableEntityObjectResult
            }

            var user = Mapper.Map<AppUser>(dto);
            await _userManager.CreateAsync(user, dto.Password);

            var userToReturn = Mapper.Map<UserDto>(user);
            return CreatedAtRoute("GetUser",
                new { id = userToReturn.Id },
                userToReturn);
        }
    }
}