using Microsoft.AspNetCore.Mvc;
using TravelGuide.Database.Entities;

namespace TravelGuide.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandmarkController : GenericController<Landmark>
    {
    }
}
