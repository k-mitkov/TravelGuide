using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelGuide.ClassLibrary.Models;
using TravelGuide.Database.Entities;
using TravelGuide.Database.Repositories;

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

        [HttpGet("GetCommentWithUsername/{landmarkId}")]
        public async Task<ActionResult<CommentWrapper>> GetCommentWithUsername([FromRoute] int landmarkId)
        {
            try
            {
                var repositoryManager = new RepositoryManager();
                var comments = await repository.SelectAllAsync(c => c.LandmarkId == landmarkId);
                List<CommentWrapper> commentWrappers = new List<CommentWrapper>();
                foreach (var comment in comments)
                {
                    var user = await repositoryManager.UsersRepository.SelectAsync(u => u.Id == comment.UserId);

                    commentWrappers.Add(new CommentWrapper()
                    {
                        Comment = comment,
                        Username = user.Username
                    });
                }

                return Ok(commentWrappers);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
