using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;

namespace TravelGuide.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : GenericController<User>
    {
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody]User user)
        {
            try
            {
                var checkUser = await repository.SelectAsync(u => u.Username == user.Username || u.Email == user.Email);

                if (checkUser != null)
                {
                    return Ok(null);
                }

                user = await repository.Insert(user);

                return Ok(user);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody]User user)
        {
            try
            {
                var checkUser = await repository.SelectAsync(u => u.Username == user.Username && u.Password == user.Password);

                if (checkUser != null)
                {
                    return Ok(checkUser);
                }

                return Ok(null);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
