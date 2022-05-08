using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;

namespace TravelGuide.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : GenericController<User>
    {
        [HttpGet("GetImageById/{id}")]
        public async Task<ActionResult<byte[]>> GetImageById([FromRoute] int id)
        {
            var user = await repository.SelectAsync(u => u.Id == id);

            return Ok(await System.IO.File.ReadAllBytesAsync(user.ImagePath));
        }
    }
}
