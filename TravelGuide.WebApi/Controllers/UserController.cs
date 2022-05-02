using Microsoft.AspNetCore.Mvc;
using TravelGuide.Database.Entities;

namespace TravelGuide.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : GenericController<User>
    {

    }
}
