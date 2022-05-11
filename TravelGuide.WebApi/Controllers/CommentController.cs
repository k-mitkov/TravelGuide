using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TravelGuide.Database.Entities;

namespace TravelGuide.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : GenericController<Comment>
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<Comment>> GetAll()
        {
            return Ok(await repository.SelectAllAsync());
        }


        [HttpPost("Post")]
        public async Task<ActionResult> Post([FromBody] Comment entity)
        {
            if (entity != null)
            {
                return Ok(await repository.Insert(entity));
            }
            return BadRequest();
        }
    }
}
