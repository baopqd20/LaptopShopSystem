using AutoMapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using LaptopShopSystem.Reponse;
using LaptopShopSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;

        public UserController(IUserRepository userRepository, ICartRepository cartRepository, IMapper mapper, IJwtRepository jwtRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserRegisterDto userCreate)
        {
            var userMap = _mapper.Map<User>(userCreate);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_userRepository.CheckEmail(userMap.Email)) {
                ModelState.AddModelError("", "Email already exists!");
                return BadRequest(ModelState);
            }
            if (userCreate.Password != userCreate.ConfirmPassword)
            {
                ModelState.AddModelError("", "Password and Confirm Password must be same");
                return BadRequest(ModelState);
            }

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something wrong while registing");
                return BadRequest(ModelState);
            }
            var cartCreate = new Cart()
            {
                UserId = userMap.Id,
            };
            if (!_cartRepository.CreateCart(cartCreate))
            {
                ModelState.AddModelError("", "Something wrong while creating cart");
                return BadRequest(ModelState);
            }

            return Ok(cartCreate);
        }
        [HttpPost("create-admin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult CreateAdmin([FromBody] UserAdminDto userCreate)
        {
            var isAdmin = User.IsInRole("admin");
            if (!isAdmin)
            {
                return Forbid();
            }
            var userMap = _mapper.Map<User>(userCreate);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_userRepository.CheckEmail(userMap.Email))
            {
                ModelState.AddModelError("", "Email already exists!");
                return BadRequest(ModelState);
            }
            if (userCreate.Password != userCreate.ConfirmPassword)
            {
                ModelState.AddModelError("", "Password and Confirm Password must be same");
                return BadRequest(ModelState);
            }

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something wrong while registing");
                return BadRequest(ModelState);
            }
            return Ok("Register Success");
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult userLogin([FromBody] UserLoginDto userLogin)
        {
            if (!_userRepository.CheckEmail(userLogin.Email))
            {
                ModelState.AddModelError("", "Email not exists");
                return BadRequest(ModelState);
            }

            var user = _userRepository.GetUserByEmail(userLogin.Email);
            if (userLogin.Password != user.Password)
            {
                ModelState.AddModelError("", "Password is incorrect");
                return BadRequest(ModelState);
            }
            var tokenString = _jwtRepository.GenerateJwtToken(userLogin.Email, _userRepository.GetRole(user), user.Id);
            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
        [Authorize("RequireAdministratorRole")]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult GetUser(int userId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var isAdmin = User.IsInRole("admin");

            if (!isAdmin&&(userIdClaim == null || int.Parse(userIdClaim) != userId))
            {
                return Forbid(); // Trả về 403 nếu không phải chính user
            }
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }

        [HttpPut("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize]
        public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDto userUpdate)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var isAdmin = User.IsInRole("admin");

            if (!isAdmin && (userIdClaim == null || int.Parse(userIdClaim) != userId))
            {
                return Forbid(); // Trả về 403 nếu không phải chính user
            }
            if (userUpdate == null)
            {
                return BadRequest("User update data is null");
            }

            if (userId <= 0)
            {
                return BadRequest("Invalid userId");
            }

            if (!_userRepository.UserExists(userId))
            {
                return NotFound("User not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFromDb = _userRepository.GetUserById(userId);
            if (userFromDb == null)
            {
                return NotFound("User not found");
            }
            var userMap = _mapper.Map(userUpdate, userFromDb);
            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return Ok(userMap);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        public IActionResult DeleteUser(int userId)
        {
            var isAdmin = User.IsInRole("admin");

            if (!isAdmin)
            {
                return Forbid(); // Trả về 403 nếu không phải chính user
            }
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var user = _userRepository.GetUserById(userId);
            if (!_userRepository.DeleteUser(user))
            {
                return BadRequest("Something went wrong while delete user");
            }
            var cart = _cartRepository.GetCartByUserId(userId);
            if (!_cartRepository.DeleteCart(cart))
            {
                return BadRequest("Something went wrong while delete cart");
            }
            return Ok("Delete user success");
        }
    }
}
