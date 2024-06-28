using AutoMapper;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;
using LaptopShopSystem.Reponse;
using LaptopShopSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShopSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;

        public UserController(IUserRepository userRepository, IMapper mapper, IJwtRepository jwtRepository)
        {
            _userRepository = userRepository;
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
            var tokenString = _jwtRepository.GenerateJwtToken(userLogin.Email, _userRepository.GetRole(user));
            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(400)]
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
        public IActionResult GetUser(int userId)
        {
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
        public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDto userUpdate)
        {
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
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }
            var user = _userRepository.GetUserById(userId);
            if (!_userRepository.DeleteUser(user))
            {
                return BadRequest("Something went wrong while delete user");
            }
            return Ok("Delete user success");
        }
    }
}
