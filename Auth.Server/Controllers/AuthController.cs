using Auth.Server.Helper;
using Auth.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private Database myDatabase = Database.Instance;

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] LoginUser user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "body is null"
                }); ;
            }

            if (myDatabase.Authenticate(user))
            {
                return Ok(new
                {
                    Message = "Login Sucess"
                });
            }
            return BadRequest(new
            {
                Message = "username or password invalid"
            });
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterUser user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "body is null"
                });
            }

            if (myDatabase.Register(user))
            {
                return Ok(new
                {
                    Message = "Sign up sucess"
                });
            }
            return BadRequest(new
            {
                Message = "user Allready register"
            });
        }

        [HttpPost("Unregister")]
        public IActionResult UnRegister([FromBody] RegisterUser user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Message = "body is null"
                });
            }

            if (myDatabase.Unregister(user))
            {
                return Ok();
            }
            return BadRequest("user not registered");
        }


    }
}
