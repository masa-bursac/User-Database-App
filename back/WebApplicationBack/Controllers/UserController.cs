using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationBack.DTO;
using WebApplicationBack.Model;
using WebApplicationBack.Repositories;
using WebApplicationBack.Services;
using WebApplicationBack.Verification;

namespace WebApplicationBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public UserService userService = new UserService();
        public UserRepository userRepository = new UserRepository();
        public UserVerification userVerification = new UserVerification();

        public UserController(MyDbContext context)
        {
            this.dbContext = context;
            userService = new UserService(new UserRepository(context));
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult Get()
        {
             userRepository.dbContext = dbContext;
             return Ok(userRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            if (!userVerification.Verify(user)) return BadRequest();
            user.Image = new byte[0];
            userService.SaveUser(user, dbContext);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User u)
        {
            if (!userVerification.VerifyLogin(u)) return BadRequest();
            User user = userService.FindByEmailAndPassword(u.Email, u.Password);
           
            if (user != null)
            {
                String jwtToken = userService.GenerateJwtToken(user);
                return Ok(jwtToken);
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            Boolean done = userService.DeleteUser(id);

            if (done) 
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("findById/{id}")]
        public IActionResult GetUser(int id)
        {
            if (id == 0) return BadRequest();
            User user = userService.FindUserById(id);
            UserDto returnUser = new UserDto(user);
            return Ok(returnUser);
        }
    }
}
